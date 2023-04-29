﻿using System;
using System.Runtime.CompilerServices;

namespace HalfMaid.Async
{
	/// <summary>
	/// An awaiter for a GameTask, which can register continuations with that GameTask
	/// to perform when the wait is complete.  This is primarily needed internally by the
	/// C# compiler to support async/await, and usually should not be explicitly invoked.
	/// This struct is designed to be inlined so that after JIT optimization, it basically
	/// ceases to exist.
	/// </summary>
	public readonly struct GameTaskAwaiter : ICriticalNotifyCompletion
	{
		/// <summary>
		/// The task this awaiter is associated with.
		/// </summary>
		public readonly GameTask Task;

		/// <summary>
		/// Construct a new awaiter for the given GameTask.
		/// </summary>
		/// <param name="task">The task that this can register continuations with.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public GameTaskAwaiter(GameTask task)
			=> Task = task;

		/// <summary>
		/// Whether the task associated with this awaiter has completed or not.
		/// </summary>
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Task.IsCompleted;
		}

		/// <summary>
		/// Get the result of the task's execution, which, for a void task, returns
		/// nothing.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if the task did not (yet)
		/// complete successfully.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
			if (Task.Status != GameTaskStatus.Success)
				throw new InvalidOperationException("Task has no result because it is either in progress or raised an exception.");
		}

		/// <summary>
		/// Register a continuation with the task that the task should invoke after the task completes.
		/// </summary>
		/// <param name="continuation">The continuation to perform after the task completes.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void OnCompleted(Action continuation)
			=> Task.SetContinuation(continuation);

		/// <summary>
		/// Register a continuation with the task that the task should invoke after the task completes.
		/// </summary>
		/// <param name="continuation">The continuation to perform after the task completes.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UnsafeOnCompleted(Action continuation)
			=> Task.SetContinuation(continuation);
	}

	/// <summary>
	/// An awaiter for a GameTask, which can register continuations with that GameTask
	/// to perform when the wait is complete.  This is primarily needed internally by the
	/// C# compiler to support async/await, and usually should not be explicitly invoked.
	/// This struct is designed to be inlined so that after JIT optimization, it basically
	/// ceases to exist.
	/// </summary>
	public readonly struct GameTaskAwaiter<T> : ICriticalNotifyCompletion
	{
		/// <summary>
		/// The task this awaiter is associated with.
		/// </summary>
		public readonly GameTask<T> Task;

		/// <summary>
		/// Construct a new awaiter for the given GameTask.
		/// </summary>
		/// <param name="task">The task that this can register continuations with.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public GameTaskAwaiter(GameTask<T> task)
			=> Task = task;

		/// <summary>
		/// Whether the task associated with this awaiter has completed or not.
		/// </summary>
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Task.IsCompleted;
		}

		/// <summary>
		/// Get the result of the task's execution, the value returned by its method.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if the task did not (yet)
		/// complete successfully.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T GetResult() => Task.Status == GameTaskStatus.Success ? Task.Result
			: throw new InvalidOperationException("Task has no result because it is either in progress or raised an exception.");

		/// <summary>
		/// Register a continuation with the task that the task should invoke after the task completes.
		/// </summary>
		/// <param name="continuation">The continuation to perform after the task completes.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void OnCompleted(Action continuation)
			=> Task.SetContinuation(continuation);

		/// <summary>
		/// Register a continuation with the task that the task should invoke after the task completes.
		/// </summary>
		/// <param name="continuation">The continuation to perform after the task completes.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UnsafeOnCompleted(Action continuation)
			=> Task.SetContinuation(continuation);
	}
}
