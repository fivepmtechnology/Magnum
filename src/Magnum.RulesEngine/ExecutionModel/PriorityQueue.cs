// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum.RulesEngine.ExecutionModel
{
	using System;
	using System.Linq;
	using Collections;
	using Extensions;

	public class PriorityQueue<T>
	{
		private readonly OrderedDictionary<int, Deque<T>> _queue;

		public PriorityQueue()
		{
			_queue = new OrderedDictionary<int, Deque<T>>();
		}

		public void Add(int priority, T action)
		{
			if (!_queue.ContainsKey(priority))
			{
				_queue.Add(priority, new Deque<T>());
			}

			_queue[priority].Add(action);
		}

		public void ExecuteAll(Action<T> callback)
		{
			T[] actions;
			do
			{
				actions = RemoveQueuedActions();

				actions.Each(callback);
			} while (actions.Length > 0);
		}

		public void Clear()
		{
			_queue.Clear();
		}

		private T[] RemoveQueuedActions()
		{
			lock (_queue)
			{
				T[] actions = _queue
					.SelectMany(x => x.Value)
					.ToArray();

				_queue.Clear();

				return actions;
			}
		}
	}
}