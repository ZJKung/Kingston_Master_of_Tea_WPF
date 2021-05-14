﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingston_Master_of_Tea_WPF_UI
{
	public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
	{
		public NotifyTaskCompletion(Task<TResult> task)
		{
			Task = task;
			if (!task.IsCompleted)
			{
				var _ = WatchTaskAsync(task);
			}
		}
		private async Task WatchTaskAsync(Task task)
		{
			try
			{
				await task;
			}
			catch
			{
			}
			var propertyChanged = PropertyChanged;
			if (propertyChanged == null)
				return;
			propertyChanged(this, new PropertyChangedEventArgs(nameof(Status)));
			propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
			propertyChanged(this, new PropertyChangedEventArgs(nameof(IsNotCompleted)));
			if (task.IsCanceled)
			{
				propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
			}
			else if (task.IsFaulted)
			{
				propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
				propertyChanged(this, new PropertyChangedEventArgs(nameof(Exception)));
				propertyChanged(this, new PropertyChangedEventArgs(nameof(InnerException)));
				propertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
			}
			else
			{
				propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));
				propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
			}
		}
		public Task<TResult> Task { get; }
		public TResult Result
		{
			get
			{
				return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default;
			}
		}
		public TaskStatus Status { get { return Task.Status; } }
		public bool IsCompleted { get { return Task.IsCompleted; } }
		public bool IsNotCompleted { get { return !Task.IsCompleted; } }
		public bool IsSuccessfullyCompleted
		{
			get
			{
				return Task.Status == TaskStatus.RanToCompletion;
			}
		}
		public bool IsCanceled { get { return Task.IsCanceled; } }
		public bool IsFaulted { get { return Task.IsFaulted; } }
		public AggregateException Exception { get { return Task.Exception; } }
		public Exception InnerException
		{
			get
			{
				return Exception?.InnerException;
			}
		}
		public string ErrorMessage
		{
			get
			{
				return InnerException?.Message;
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
