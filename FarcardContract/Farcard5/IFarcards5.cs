using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using FarcardContract.Data;
using FarcardContract.Data.Farcard5;

namespace FarcardContract.Farcard5
{
	public interface IFarcards5 : IDisposable
	{
		void Init();
		void Done();

		int GetCardInfoL(UInt64 card,
			UInt32 restaurant,
			UInt32 unitNo,
			ref CardInfoL cardInfo);

		int TransactionL(UInt32 account,
			TransactionInfoL transactionInfo);

		int GetCardImageL(UInt32 account, ref TextInfo info);

		int FindEmail(string email, ref HolderInfo holderInfo);

		void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr);

		void AnyInfo(byte[] inpBuf, out byte[] outBuf);

		int GetDiscLevelInfoL(UInt32 account, ref DiscLevelInfo info);

		int GetCardMessageL(UInt32 account, ref TextInfoEx info);

		int GetCardMessage2L(UInt32 account, ref TextInfoEx info);

		int CheckInfoL(UInt32 account, byte[] inpBuf);

		int TransactionPacketL(List<TransactionPacketInfoL> transactions);
	}
}
