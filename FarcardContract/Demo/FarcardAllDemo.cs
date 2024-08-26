using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Security.Principal;
using FarcardContract.Data;
using FarcardContract.Data.Farcard5;
using FarcardContract.Data.Farcard6;
using FarcardContract.Demo.Farcard5;
using FarcardContract.Demo.Farcard6;

namespace FarcardContract.Demo
{
	[Export(typeof(IFarcards))]
	internal class FarcardAllDemo : IFarcards
	{
		private readonly Farcard6Demo _farcard6Demo = new Farcard6Demo();
		private readonly Farcard5Demo _farcard5Demo = new Farcard5Demo();
		private readonly Logger<FarcardAllDemo> _logger = new Logger<FarcardAllDemo>();

		public void Init()
		{
			try
			{
				_farcard5Demo.Init();
			}
			catch (Exception ex)
			{

				_logger.Error(ex);
			}
			try
			{
				_farcard6Demo.Init();
			}
			catch (Exception ex)
			{

				_logger.Error(ex);
			}
		}

		public int GetCardInfoEx(long card, uint restaurant, uint unitNo, ref CardInfoEx cardInfo, Byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
		{
			outBuf = null;
			outKind = 0;
			var res = 1;
			try
			{
				res = _farcard6Demo.GetCardInfoEx(card, restaurant, unitNo, ref cardInfo, inpBuf, inpKind,
					out outBuf, out outKind);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return res;
		}

		public int GetCardInfoL(ulong card, uint restaurant, uint unitNo, ref CardInfoL cardInfo)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.GetCardInfoL(card, restaurant, unitNo, ref cardInfo);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return res;
		}

		public int TransactionsEx(List<TransactionInfoEx> transactionInfo, Byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
		{

			outBuf = null;
			outKind = 0;
			var res = 1;
			try
			{
				res = _farcard6Demo.TransactionsEx(transactionInfo, inpBuf, inpKind, out outBuf, out outKind);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int TransactionL(uint account, TransactionInfoL transactionInfo)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.TransactionL(account, transactionInfo);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int FindEmail(string email, ref HolderInfo holderInfo)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.FindEmail(email, ref holderInfo);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			if (res != 0)
			{
				try
				{
					res = _farcard6Demo.FindEmail(email, ref holderInfo);
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
				}
			}
			return res;
		}

		public void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr)
		{
			try
			{
				_farcard5Demo.FindCardsL(findText, cbFind, backPtr);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			try
			{
				_farcard6Demo.FindCardsL(findText, cbFind, backPtr);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

		}

		public void FindAccountsByKind(FindKind kind, string findText, CBFind cbFind, IntPtr backPtr)
		{
			try
			{
				_farcard6Demo.FindAccountsByKind(kind, findText, cbFind, backPtr);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public void AnyInfo(byte[] inpBuf, out byte[] outBuf)
		{
			outBuf = null;
			try
			{
				_farcard5Demo.AnyInfo(inpBuf, out outBuf);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			if (outBuf == null)
			{
				try
				{
					_farcard6Demo.AnyInfo(inpBuf, out outBuf);
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
				}
			}
		}

		public int GetDiscLevelInfoL(uint account, ref DiscLevelInfo info)
		{
			var res = 1;

			try
			{
				res = _farcard5Demo.GetDiscLevelInfoL(account, ref info);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			if (res != 0)
			{
				try
				{
					res = _farcard6Demo.GetDiscLevelInfoL(account, ref info);
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
				}
			}

			return res;
		}

		public void Done()
		{
			try
			{
				_farcard5Demo.Done();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			try
			{
				_farcard6Demo.Done();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public int GetCardImageEx(long card, ref TextInfo info)
		{
			var res = 1;
			try
			{
				res = _farcard6Demo.GetCardImageEx(card, ref info);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int GetCardImageL(uint account, ref TextInfo info)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.GetCardImageL(account, ref info);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int GetCardMessageL(uint account, ref TextInfoEx info)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.GetCardMessageL(account, ref info);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int GetCardMessage2L(uint account, ref TextInfoEx info)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.GetCardMessage2L(account, ref info);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int CheckInfoL(uint account, byte[] inpBuf)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.CheckInfoL(account, inpBuf);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public int TransactionPacketL(List<TransactionPacketInfoL> transactions)
		{
			var res = 1;
			try
			{
				res = _farcard5Demo.TransactionPacketL(transactions);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			return res;
		}

		public void Dispose()
		{
			_logger.Trace("Begin Dispose FarcardsAll Demo");
			try
			{
				_logger.Trace("Begin Dispose Farcards5Demo");
				_farcard5Demo.Dispose();
				_logger.Trace("Complete Dispose Farcards5Demo");
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			try
			{
				_logger.Trace("Begin Dispose Farcards6Demo");
				_farcard6Demo.Dispose();
				_logger.Trace("Complete Dispose Farcards6Demo");
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			_logger.Trace("End Dispose FarcardsAll Demo");
		}


	}
}
