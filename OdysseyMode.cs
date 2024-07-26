using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OdysseyMode
{

    public class HideMinimap : ModSystem
    {
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            for (int i = 0; i < layers.Count; i++)
            {
				//Terraria.UI.Chat.ChatManager.AddChatText(null, layers[i].Name, Terraria.UI.Chat.ChatManager.ShadowDirections[0]);
				//layers.ElementAt(i).Active = false;
                //Remove Resource bars
                if (layers[i].Name.Contains("Inventory")) // https://github-wiki-see.page/m/tModLoader/tModLoader/wiki/Vanilla-Interface-layers-values
                {
					//layers[i].Draw
					//layers.RemoveAt(i);
                }
            }
        }

        public override void Load()
        {
			// Ensure the CustomUnlockable class is loaded
            CustomUnlockable customUnlockable = new CustomUnlockable();
            customUnlockable.Load();
			
			//Terraria.GameContent.Creative.CreativePowers.;
			//Main.MenuUI.;
			//Main.CreativeMenu.Draw()
			//Main.MenuUI += Test;
			//Main.menuMode = 0;
 
			//Terraria.GameContent.Creative.CreativePowers.GodmodePower power =
			//Terraria.GameContent.Creative.CreativePowerManager.Instance.GetPower<Terraria.GameContent.Creative.CreativePowers.GodmodePower>();
			
			//power.

            // Use reflection to access private fields in Main
            //FieldInfo buttonField = typeof(Main).GetField("invisibleButton", BindingFlags.NonPublic | BindingFlags.Instance);
            //List<UIElement> buttons = (List<UIElement>)buttonField.GetValue(Main.instance);

//			.SetEnabledState() = 0;


        	
        }
/*
        private void CreativeUI_Update(On.Terraria.UI.CreativeUI.orig_Update orig, CreativeUI self, GameTime gameTime)
        {
            // Call the original method to ensure normal update functionality
            orig(self, gameTime);

            // Remove the Personal Power Menu from the Creative UI
            self.personalPowerMenu = null;
        }

		private void Test() {

		}
*/
		
    }

	public class CustomUnlockable : ModSystem
    {
		private Hook _getIsUnlockedHook;
        public override void Load()
        {
            // Assuming the class and method are in Terraria namespace.
            // Adjust the namespace and class name as per your requirements.
            Type targetType = typeof(Terraria.GameContent.Creative.CreativePowers.GodmodePower); // Replace with the actual class
            MethodInfo method = targetType.GetMethod("GetIsUnlocked", BindingFlags.Public | BindingFlags.Instance);

            if (method != null)
            {
                _getIsUnlockedHook = new Hook(method, new Func<Terraria.GameContent.Creative.CreativePowers.GodmodePower, bool>(OverrideGetIsUnlocked));
            
                //HookEndpointManager.Add(method, new Func<SomeClass, bool>(OverrideGetIsUnlocked));
            }
        }

        private bool OverrideGetIsUnlocked(Terraria.GameContent.Creative.CreativePowers.GodmodePower instance)
        {
            // Custom logic to determine if it should be unlocked or not
            // Return false as per your requirement
            return false;
        }
    }

}
