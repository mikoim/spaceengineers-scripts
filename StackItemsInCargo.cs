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

            Echo($"{cargo.CustomName}: cleaned");

            var inventory = cargo.GetInventory(0);
            var items = inventory.GetItems();

            for (int itemIndex = items.Count - 1; itemIndex >= 0; itemIndex--)
                    inventory.TransferItemTo(inventory, itemIndex, stackIfPossible: true);
        }

        Echo("Finish!");
    }

    #endregion
}
