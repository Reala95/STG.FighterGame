using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClassLib
{
    public class _StaticData
    {
        public static int selectedFighter = 2;
        public const int solor = 0;
        public const int vortex = 1;
        public const int nebula = 2;
        public const int stardust = 3;

        public static string playerFighterTag = "Player/Fighter";
        public static string playerWeaponTag = "Player/Weapon";
        public static string playerSkillTag = "Player/Skill";
        public static string playerSafeShieldTag = "Player/SafeShield";
        public static string enemyFighterTag = "Enemy/Fighter";
        public static string enemyWeaponTag = "Enemy/Weapon";
        public static string bulletPlayerTag = "Bullet/Player";
        public static string bulletEnemyTag = "Bullet/Enemy";
        public static string uiHealBarTag = "UI/HealBar";
        public static string uiSPBarTag = "UI/SPBar";
        public static string uiManager = "UI/Manager";
        public static string specialSolorShieldTag = "Special/SolorShield";

        public static Vector3 itemVelocity = new Vector3(0, -0.5f, 0);
    }
}
