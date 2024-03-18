using System;

// ReSharper disable IdentifierTypo

public readonly struct DispatchableTaskBundle
{
    public readonly Action Task;

    public DispatchableTaskBundle(Action task)
    {
        Task = task;
    }
}