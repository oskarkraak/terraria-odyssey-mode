using System;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace OdysseyMode
{
    public class PowerLocker : ModSystem
    {
        public override void Load()
        {
            setMasterMode();
            LockPower<CreativePowers.GodmodePower>();
            LockPower<CreativePowers.DifficultySliderPower>();
            LockPower<CreativePowers.FreezeTime>();
            LockPower<CreativePowers.FarPlacementRangePower>();
            LockPower<CreativePowers.StopBiomeSpreadPower>();
            LockPower<CreativePowers.SpawnRateSliderPerPlayerPower>();
        }

        private void setMasterMode() {
            CreativePowers.DifficultySliderPower difficultySliderPower = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
            Type targetType = typeof(CreativePowers.ASharedSliderPower);
            FieldInfo field;
            field = targetType.GetField("_currentTargetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(difficultySliderPower, 1f);
            field = targetType.GetField("_needsToCommitChange", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(difficultySliderPower, true);
        }

        public static void LockPower<T>() where T : ICreativePower
        {
            Type targetType = typeof(T);
            T instance = CreativePowerManager.Instance.GetPower<T>();
            if (instance == null)
            {
                throw new InvalidOperationException($"Unable to create an instance of '{targetType.Name}'.");
            }

            PropertyInfo propertyInfo = targetType.GetProperty("DefaultPermissionLevel", BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(instance, PowerPermissionLevel.LockedForEveryone);
            }
            else
            {
                throw new ArgumentException($"Property 'DefaultPermissionLevel' not found or is not writable on type '{targetType.Name}'.");
            }
        }
    }
}
