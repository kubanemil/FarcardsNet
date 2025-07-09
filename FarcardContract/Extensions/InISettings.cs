using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarcardContract.Extensions
{
    public class InISettings<T> : InIFile
    {
        static Dictionary<InIPropAttribute, PropertyInfo> props;
        static InISettings()
        {
            if(props == null)
                props = new Dictionary<InIPropAttribute, PropertyInfo>();
            var type = typeof(T);
            var prop = type.GetProperties().Where(x=>x.CanWrite && x.CanRead);
            foreach (var x in prop)
            {
                var attr = x.GetCustomAttribute<InIPropAttribute>();
                if(attr == null)
                    continue;
                props.Add(attr, x);
            }
        }

        private static readonly Logger<InISettings<T>> _logger = new Logger<InISettings<T>>(1);

        private string _path;

        private string SettingsPath { get {return _path; } set { _path = value; } }

        static string GetSettingsPath()
        {
            Type tm = typeof(T);
            string settingPath = Path.Combine(Path.GetDirectoryName(tm.Assembly.Location),
           tm.Name + ".ini"
           );
            return settingPath;
        }

        public static T GetSettings(string path = null)
        {
            return LoadData(path);
        }

        private static T LoadData(string path = null)
        {
            var res = (T)Activator.CreateInstance(typeof(T));
            var s = new InISettings<T>();
            
            if (string.IsNullOrWhiteSpace(path))
                path = GetSettingsPath();
            

            try
            {
                var fi = new FileInfo(path);
                if(!new FileInfo(path).Exists)
                    throw new FileNotFoundException(path);
                var propSections = props.Keys.GroupBy(x=>x.Section).Select(x=>x.Key).ToList();
                var sections = GetPrivateProfileSectionNames(path)?.Where(x=> propSections.Contains(x));
                foreach (var section in sections)
                {
                    var sectionsData = GetPrivateProfileSection(path,section);
                    if (sectionsData?.Count == 0)
                        continue;
                    foreach (var sectionData in sectionsData)
                    {
                        var propattr = new InIPropAttribute(section, sectionData.Key);

                        PropertyInfo prop = null;
                           props.TryGetValue(propattr,out prop);
                        if (prop != null)
                        {
                            Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            prop.SetValue(res, sectionData.Value == null ?null: Convert.ChangeType(sectionData.Value,t),null);
                        }
                    }
                }
            }
            catch (FileNotFoundException) {
                _logger.Error($"Error on loadData from ini path: {path}");
                s.Save();
                res = LoadData(path);
            }
            return res;
        }


        private void Load(string path = null)
        {
            try
            {
                var temp = LoadData(path);
                var pr = this.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead);
                var tempType = temp.GetType();
                foreach (var prop in pr)
                {
                    var val = tempType.GetProperty(prop.Name).GetValue(temp, null);
                    prop.SetValue(this, val, null);
                }
            }
            catch (FileNotFoundException) 
            {

            }
            
        }
        public void Save() 
        {

        }
    }
}
