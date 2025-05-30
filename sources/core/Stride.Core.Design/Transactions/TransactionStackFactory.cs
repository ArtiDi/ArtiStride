// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Core.Transactions;

/// <summary>
/// A static factory to create <see cref="ITransactionStack"/> instances.
/// </summary>
public static class TransactionStackFactory
{
    public static ITransactionStack Create(int capacity)
    {
        return new TransactionStack(capacity);
    }
}
