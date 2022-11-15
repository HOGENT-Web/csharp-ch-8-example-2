using System;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Layout;

public partial class Header
{
    private bool isOpen;
    private string? isOpenClass => isOpen ? "is-active" : null;

    private void ToggleMenuDisplay()
    {
        isOpen = !isOpen;
    }
}

