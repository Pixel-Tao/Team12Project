using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Player player = (Player)target;
        if (GUILayout.Button("Fix Camera Facing"))
        {
            player.FixCameraFacing();
        }

        if (Application.isPlaying)
        {
            List<ItemEntity> items = Managers.DB.GetAll<ItemEntity>();
            items = items.Where(s => s.itemType == Defines.ItemType.Equipment).ToList();
            GUILayout.Space(20);
            GUILayout.Label("콤보 무기 장착");
            DrawComboWeaponButtons(items);
            DrawUnEquipButton(Defines.ItemEquipmentType.Weapon, Defines.CharacterCombatStyleType.ComboAttack);
            
            GUILayout.Space(20);
            GUILayout.Label("스킬 무기 장착");
            DrawWeaponButtons(items);
            DrawUnEquipButton(Defines.ItemEquipmentType.Weapon);
            
            GUILayout.Space(20);
            GUILayout.Label("방어구 장착");
            DrawEquipableButtons(items, Defines.ItemEquipmentType.Armor);
            DrawUnEquipButton(Defines.ItemEquipmentType.Armor);
            
            GUILayout.Space(20);
            GUILayout.Label("악세사리 장착");
            DrawEquipableButtons(items, Defines.ItemEquipmentType.Accessory);
            DrawUnEquipButton(Defines.ItemEquipmentType.Accessory);
        }
    }
    
    private void DrawEquipableButtons(List<ItemEntity> items, Defines.ItemEquipmentType equipmentType)
    {
        foreach (var item in items)
        {
            if (item.equipableEntity.equipmentType == equipmentType)
                DrawGenerateButton(item);
        }
    }

    private void DrawComboWeaponButtons(List<ItemEntity> items)
    {
        foreach (var item in items)
        {
            if (item.equipableEntity.combatStyleType == Defines.CharacterCombatStyleType.None) continue;
            if (item.equipableEntity.combatStyleType == Defines.CharacterCombatStyleType.ComboAttack)
                DrawGenerateButton(item);
        }
    }

    private void DrawWeaponButtons(List<ItemEntity> items)
    {
        foreach (var item in items)
        {
            if (item.equipableEntity.combatStyleType == Defines.CharacterCombatStyleType.None) continue;
            if (item.equipableEntity.combatStyleType != Defines.CharacterCombatStyleType.ComboAttack)
                DrawGenerateButton(item);
        }
    }

    private void DrawGenerateButton(ItemEntity item)
    {
        if (GUILayout.Button($"{item.displayTitle} 장착"))
        {
            Player player = (Player)target;
            player.gameObject.name = nameof(Player);
            player.Equipment.Equip(item);
        }
    }
    
    private void DrawUnEquipButton(Defines.ItemEquipmentType equipmentType, Defines.CharacterCombatStyleType combatStyleType = Defines.CharacterCombatStyleType.None)
    {
        if (GUILayout.Button($"장착 해제"))
        {
            Player player = (Player)target;
            player.Equipment.UnEquip(equipmentType, combatStyleType);
        }
    }
}