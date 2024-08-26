using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using FarcardContract.Data;
using FarcardContract.Data.Farcard5;
using FarcardContract.Farcard5;

namespace FarcardContract.Demo.Farcard5
{
	[Export(typeof(IFarcards5))]
	public class Farcard5Demo : IFarcards5
	{
		private readonly Logger<Farcard5Demo> _logger = new Logger<Farcard5Demo>();
		bool UI = false;
		public void Init()
		{
			_logger.Trace("Begin init");
			try
			{
				var app = Process.GetCurrentProcess();

				_logger.Trace($"start app {app.Id} : {app.ProcessName}");
				foreach (ProcessModule module in app.Modules)
				{
					_logger.Trace($"Loaded modules: {module.ModuleName} {module.EntryPointAddress}");
				}
				_logger.Trace("Load Demo Processor");
				UI = Environment.UserInteractive;
				if (UI)
				{
					//    var t = new Thread(startform);
					//    t.IsBackground = false;
					//    t.SetApartmentState(ApartmentState.STA);
					//    t.Start();
					//    Thread.Sleep(100);
				}
			}
			catch { }
			_logger.Trace("End init");
		}

		public void Done()
		{
			_logger.Trace("Call done");
		}

		public int GetCardInfoL(ulong card, uint restaurant, uint unitNo, ref CardInfoL cardInfo)
		{
			_logger.Trace("GetCardInfoLDemo");
			if (card == 1)
			{
				cardInfo.Demo();
				var results = cardInfo.ToStringLog();

				_logger.Trace(results);
				_logger.Trace("result: 0");
				return 0;
			}
			_logger.Trace(cardInfo.ToStringLog());
			_logger.Trace("result: 1");
			return 1;
		}

		public int TransactionL(uint account, TransactionInfoL transactionInfo)
		{
			_logger.Trace("TransactionLDemo");

			_logger.Trace(transactionInfo.ToStringLog());

			_logger.Trace("result 0");
			return 0;
		}

		public int GetCardImageL(uint account, ref TextInfo info)
		{
			_logger.Trace("GetCardImageExDemo");
			if (account == ExtensionDemo.GetDemoAccount)
			{
				var photo = Properties.Resources.no_photo;
				var photoPath = new FileInfo(nameof(Properties.Resources.no_photo) + ".jpg");
				try
				{
					using (var fs = new FileStream(photoPath.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
					{
						photo.Save(fs, ImageFormat.Jpeg);
						info.Text = photoPath.FullName;
						return 0;
					}
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
				}
			}

			return 1;
		}

		public int FindEmail(string email, ref HolderInfo holderInfo)
		{
			_logger.Trace($"FindEmailDemo email: {email}");
			if ("test@test.ru".Equals(email, StringComparison.InvariantCultureIgnoreCase))
			{
				holderInfo.Demo();
				_logger.Trace(holderInfo.ToStringLog());
				_logger.Trace("result 0");
				return 0;
			}
			_logger.Trace(holderInfo.ToStringLog());
			_logger.Trace("result 1");
			return 1;
		}

		public void AnyInfo(byte[] inpBuf, out byte[] outBuf)
		{
			_logger.Trace($"AnyInfo");
			outBuf = null;
			if (inpBuf != null && inpBuf.Length > 0)
				_logger.Trace($"InputBuf:{Encoding.UTF8.GetString(inpBuf)}");
		}

		public int GetDiscLevelInfoL(uint account, ref DiscLevelInfo info)
		{
			_logger.Trace($"GetDiscLevelInfoL account: {account}");
			int res = 1;
			if (account == ExtensionDemo.GetDemoAccount)
			{
				info.Demo();
				res = 0;
			}

			return res;
		}

		public void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr)
		{
			_logger.Trace($"FindCardsLDemo {findText}");
			var info = new HolderInfo().Demo();
			if (findText != null)
				if (info.Card.ToString().Contains(findText)
					|| info.Owner.ToLower().Contains(findText.ToLower()))
				{
					_logger.Trace($"Find account by text: {findText} ");
					_logger.Trace(info.ToStringLog());
					cbFind?.Invoke(backPtr, info.ClientId, info.Card, info.Owner);
				}

		}
			

		public int GetCardMessageL(uint account, ref TextInfoEx info)
		{
			_logger.Trace($"GetCardMessageL account: {account}");
			int res = 1;
			if (account == ExtensionDemo.GetDemoAccount)
			{
				info.Text = ExtensionDemo.GetDemoScrMessage;
				res = 0;
			}
			return res;
		}

		public int GetCardMessage2L(UInt32 account, ref TextInfoEx info)
		{
			_logger.Trace($"GetCardMessage2L account: {account}");
			int res = 1;
			if (account == ExtensionDemo.GetDemoAccount)
			{
				info.Text = ExtensionDemo.GetDemoPrnMessage;
				res = 0;
			}
			return res;
		}
		
		public int CheckInfoL(uint account, byte[] inpBuf)
		{
			_logger.Trace($"CheckInfoL");			
			if (inpBuf != null && inpBuf.Length > 0)
				_logger.Trace($"InputBuf:{Encoding.UTF8.GetString(inpBuf)}");
			return 0;
		}

		public int TransactionPacketL(List<TransactionPacketInfoL> transactions)
		{
			foreach (TransactionPacketInfoL trans in transactions)
			{
				var res = TransactionL(trans.Account, trans.Info);
				if (res != 0)
					return res;
			}
			return 0;
		}

		public void Dispose()
		{
			_logger.Trace("Dispose Farcards5 Demo");
		}

	}
}
