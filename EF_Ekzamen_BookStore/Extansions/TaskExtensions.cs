﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Ekzamen_BookStore.Extansions
{
	internal static class TaskExtensions
	{
		public static async void FireAndForgetSafeAsync(this Task task)
		{
			try
			{
				await task.ConfigureAwait(false);
			}
			catch (Exception exception)
			{
				// ignored
			}
		}
	}
}
