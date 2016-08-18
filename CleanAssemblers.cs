#region

using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;

#endregion

internal class CleanAssemblers : MyGridProgram
{
    #region Programmable Block

    private void Main()
    {
        var assemblerList = new List<IMyTerminalBlock>();
        GridTerminalSystem.GetBlocksOfType<IMyAssembler>(assemblerList);

        if (assemblerList.Count == 0)
        {
            Echo("Assembler not found");
            return;
        }

        var cargoList = new List<IMyTerminalBlock>();
        GridTerminalSystem.GetBlocksOfType<IMyCargoContainer>(cargoList);
        IMyCargoContainer cargo = null;

        for (var cargoIndex = 0; cargoIndex < cargoList.Count; cargoIndex++)
        {
            if (!cargoList[cargoIndex].CustomName.Contains("#origin")) continue;
            cargo = cargoList[cargoIndex] as IMyCargoContainer;
            Echo("Cargo found: " + cargo.CustomName);
            break;
        }

        if (cargo == null)
        {
            Echo("Cargo not found");
            return;
        }

        for (var assemblerIndex = 0; assemblerIndex < assemblerList.Count; assemblerIndex++)
        {
            var assembler = assemblerList[assemblerIndex];
            var transferedItemCount = 0;

            for (var inventoryIndex = assembler.GetInventoryCount() - 1; inventoryIndex >= 0; inventoryIndex--)
            {
                var inventory = assembler.GetInventory(inventoryIndex);
                var items = inventory.GetItems();

                transferedItemCount += items.Count;

                for (var itemIndex = items.Count - 1; itemIndex >= 0; itemIndex--)
                {
                    Echo(inventory.TransferItemTo(cargo.GetInventory(0), itemIndex, stackIfPossible: true).ToString());
                }
            }

            if (transferedItemCount > 0)
                Echo($"{transferedItemCount} items transferred from {assembler.CustomName}");
        }

        Echo("Finish!");
    }

    #endregion
}