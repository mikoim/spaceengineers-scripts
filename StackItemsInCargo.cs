#region

using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;

#endregion

internal class StackItemsInCargo : MyGridProgram
{
    #region Programmable Block

    private void Main()
    {
        var cargosList = new List<IMyTerminalBlock>();
        GridTerminalSystem.GetBlocksOfType<IMyCargoContainer>(cargosList);

        for (var cargoIndex = 0; cargoIndex < cargosList.Count; cargoIndex++)
        {
            var cargo = cargosList[cargoIndex];

            if (!cargo.CustomName.Contains("#stack")) continue;

            var inventory = cargo.GetInventory(0);
            var items = inventory.GetItems();

            for (var itemIndex = 0; itemIndex < items.Count; itemIndex++)
                inventory.TransferItemTo(inventory, itemIndex, stackIfPossible: true);
        }
    }

    #endregion
}