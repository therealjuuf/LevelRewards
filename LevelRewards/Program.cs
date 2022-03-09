using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using xLevelRewards;

namespace xLevelReward
{
    static class Program
    {
        static void Main()
        {
			LevelReward[] LevelRewards = DB.ReadLevelRewards().ToArray();
            WriteToService(LevelRewards);
        }

        private static void WriteToService(LevelReward[] LevelRewards)
        {


			Process[] Processes = Process.GetProcessesByName("ps_game");

            if (Processes.Length == 0)
            {
                MessageBox.Show("No ps_game process was found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MemoryManager.ProcessID = Processes[0].Id;

            List<byte> Buffer = new List<byte>();

            Buffer.AddRange(BitConverter.GetBytes(LevelRewards.Length));

            foreach (LevelReward si in LevelRewards)
                Buffer.AddRange(LevelRewardToBuffer(si));

            byte[] Memory = Buffer.ToArray();

            int Address = MemoryManager.AllocateMemory(Memory.Length);

            MemoryManager.WriteProcessMemory(Address, Memory);
            MemoryManager.WriteProcessMemory(0x00A00018, BitConverter.GetBytes(Address));
        }

        private static List<byte> LevelRewardToBuffer(LevelReward si)
        {
            List<byte> Buffer = new List<byte>();


            Buffer.AddRange(BitConverter.GetBytes(si.Level));
            Buffer.AddRange(BitConverter.GetBytes(si.Faction));
            Buffer.AddRange(BitConverter.GetBytes(si.Family));
            Buffer.AddRange(BitConverter.GetBytes(si.Job));
            Buffer.AddRange(BitConverter.GetBytes(si.Type));
            Buffer.AddRange(BitConverter.GetBytes(si.TypeID));
            Buffer.AddRange(BitConverter.GetBytes(si.Count));

            return Buffer;
        }
    }
}
