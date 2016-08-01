using System;
using System.Linq;
using System.Collections.Generic;

namespace DrawingApplication
{
	public static class ListExtensions
	{
		/// <summary>
		/// Asdfasdf
		/// </summary>
		public enum MoveDirection
		{
			Up, Down
		}

		/// <summary>
		/// Move object up or down in collection
		/// </summary>
		/// <param name="list">collection to edit</param>
		/// <param name="iIndexToMove">Objects index</param>
		/// <param name="direction">Direction to move</param>
		public static void Move<T>(this IList<T> list, int iIndexToMove, MoveDirection direction)
		{
            if (iIndexToMove == 0 && direction == MoveDirection.Down)
                return;
            else if (iIndexToMove == (list.Count - 1) && direction == MoveDirection.Up)
                return;
            else if (iIndexToMove >= list.Count || iIndexToMove < 0)
                return;

			if (direction == MoveDirection.Down)
			{
				try
				{
					var old = list[iIndexToMove - 1];
					list[iIndexToMove - 1] = list[iIndexToMove];
					list[iIndexToMove] = old;
				}
				catch
				{
#if DEBUG
                    throw;
#elif RELEASE
					return;
#endif
				}
			}
			else
			{
				try
				{
					var old = list[iIndexToMove + 1];
					list[iIndexToMove + 1] = list[iIndexToMove];
					list[iIndexToMove] = old;
				}
				catch
				{
#if DEBUG
                    throw;
#elif RELEASE
					return;
#endif
				}
			}
		}

		/// <summary>
		/// Move object to specified index
		/// </summary>
		/// <param name="list">collection to edit</param>
		/// <param name="iIndexToMove">Objects index</param>
		/// <param name="iNewPosition">Position to send object</param>
		public static void MoveTo<T>(this IList<T> list, int iIndexToMove, int iNewPosition)
		{
			try
			{
				if (iIndexToMove >= list.Count) return;
				//if (iIndexToMove == list.Count - 1) return;
                if (iIndexToMove < 0) return;
				if (iIndexToMove == iNewPosition) return;
				try
				{
					T obj = list[iIndexToMove];
					list.RemoveAt(iIndexToMove);
					list.Insert(iNewPosition, obj);
				}
				catch
				{
#if DEBUG
                    throw;
#elif RELEASE
					return;
#endif
				}
			}
			catch
			{
#if DEBUG
                throw;
#elif RELEASE
				return;
#endif
			}
		}

		/// <summary>
		/// Send object to begining of collection
		/// </summary>
		/// <param name="list">Collection to edit</param>
		/// <param name="iObjectToMove">Instance of object to move</param>
		public static void SendToBack<T>(this IList<T> list, T iObjectToMove)
		{
            if(list.Count > 1 && iObjectToMove != null)
			    SendToBack(list, list.IndexOf(iObjectToMove));
		}

		/// <summary>
		/// Send object to begining of collection
		/// </summary>
		/// <param name="list">Collection to edit</param>
		/// <param name="iIndexToMove">Index of object to be moved</param>
		public static void SendToBack<T>(this IList<T> list, int iIndexToMove)
		{
            if(list.Count > 1)
			    list.MoveTo(iIndexToMove, 0);
		}

		/// <summary>
		/// Sned object to end of collection
		/// </summary>
		/// <param name="list">Collection to edit</param>
		/// <param name="iObjectToMove">Instance of object to move</param>
		public static void BringToFront<T>(this IList<T> list, T iObjectToMove)
		{
            if(list.Count > 1 && iObjectToMove != null)
			    BringToFront(list, list.IndexOf(iObjectToMove));
		}

		/// <summary>
		/// Sned object to end of collection
		/// </summary>
		/// <param name="list">Collection to edit</param>
		/// <param name="iIndexToMove">Idex of object to be moved</param>
		public static void BringToFront<T>(this IList<T> list, int iIndexToMove)
		{
			list.MoveTo(iIndexToMove, list.Count-1);
		}

		public static void AddAtBeginning<T>(this IList<T> list, T item)
		{
			list.Insert(0, item);
		}

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
	}
}