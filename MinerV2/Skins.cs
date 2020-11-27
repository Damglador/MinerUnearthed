﻿using System;
using UnityEngine;
using R2API;
using RoR2;
using R2API.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MinerPlugin
{
    public static class Skins
    {
        private static SkinDef.GameObjectActivation[] getActivations(GameObject[] allObjects, params GameObject[] activatedObjects)
        {
            List<SkinDef.GameObjectActivation> GameObjectActivations = new List<SkinDef.GameObjectActivation>();

            for (int i = 0; i < allObjects.Length; i++)
            {

                bool activate = activatedObjects.Contains(allObjects[i]);

                GameObjectActivations.Add(new SkinDef.GameObjectActivation
                {
                    gameObject = allObjects[i],
                    shouldActivate = activate
                });
            }

            return GameObjectActivations.ToArray();
        }

        public static void RegisterSkins()
        {
            GameObject bodyPrefab = MinerPlugin.characterPrefab;

            GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();

            ModelSkinController skinController = model.AddComponent<ModelSkinController>();
            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            SkinnedMeshRenderer mainRenderer = Reflection.GetFieldValue<SkinnedMeshRenderer>(characterModel, "mainSkinnedMeshRenderer");

            GameObject diamondPickL = childLocator.FindChild("DiamondPickL").gameObject;
            GameObject diamondPickR = childLocator.FindChild("DiamondPickR").gameObject;

            GameObject[] allObjects = new GameObject[]
            {
                diamondPickL,
                diamondPickR
            };

            LanguageAPI.Add("MINERBODY_DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add("MINERBODY_MOLTEN_SKIN_NAME", "Molten");
            LanguageAPI.Add("MINERBODY_PUPLE_SKIN_NAME", "Puple");
            LanguageAPI.Add("MINERBODY_TUNDRA_SKIN_NAME", "Tundra");
            LanguageAPI.Add("MINERBODY_IRON_SKIN_NAME", "Iron");
            LanguageAPI.Add("MINERBODY_GOLD_SKIN_NAME", "Gold");
            LanguageAPI.Add("MINERBODY_DIAMOND_SKIN_NAME", "Diamond");
            LanguageAPI.Add("MINERBODY_STEVE_SKIN_NAME", "Minecraft");

            LoadoutAPI.SkinDefInfo skinDefInfo = default(LoadoutAPI.SkinDefInfo);
            skinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            skinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            skinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];

            skinDefInfo.GameObjectActivations = getActivations(allObjects);

            skinDefInfo.Icon = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<ModelSkinController>().skins[0].icon;
            skinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = mainRenderer.sharedMesh
                }
            };
            skinDefInfo.Name = "MINERBODY_DEFAULT_SKIN_NAME";
            skinDefInfo.NameToken = "MINERBODY_DEFAULT_SKIN_NAME";
            skinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            skinDefInfo.RootObject = model;
            skinDefInfo.UnlockableName = "";

            CharacterModel.RendererInfo[] rendererInfos = skinDefInfo.RendererInfos;
            CharacterModel.RendererInfo[] array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            Material commandoMat = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial;

            //clone commando material for that spicy hopoo shader
            Material material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(commandoMat);
                material.SetColor("_Color", Color.white);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMiner").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0.4f);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMiner").GetTexture("_EmissionMap"));
                material.SetFloat("_NormalStrength", 0);
                //material.SetTexture("_NormalTex", Assets.mainAssetBundle.LoadAsset<Material>("matMiner").GetTexture("_BumpMap"));

                array[0].defaultMaterial = material;
            }

            //and do the same for the diamond picks
            material = array[1].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(commandoMat);
                material.SetColor("_Color", Color.white);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matDiamondPick").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0f);
                material.SetFloat("_NormalStrength", 0);

                array[1].defaultMaterial = material;
            }

            material = array[2].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(commandoMat);
                material.SetColor("_Color", Color.white);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matDiamondPick").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0f);
                material.SetFloat("_NormalStrength", 0);

                array[2].defaultMaterial = material;
            }

            skinDefInfo.RendererInfos = array;

            SkinDef defaultSkin = LoadoutAPI.CreateNewSkinDef(skinDefInfo);

            LoadoutAPI.SkinDefInfo moltenSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            moltenSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            moltenSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            moltenSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];

            moltenSkinDefInfo.GameObjectActivations = getActivations(allObjects);

            moltenSkinDefInfo.Icon = Assets.mainAssetBundle.LoadAsset<Sprite>("texMoltenAchievement");
            moltenSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = mainRenderer.sharedMesh
                }
            };
            moltenSkinDefInfo.Name = "MINERBODY_MOLTEN_SKIN_NAME";
            moltenSkinDefInfo.NameToken = "MINERBODY_MOLTEN_SKIN_NAME";
            moltenSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            moltenSkinDefInfo.RootObject = model;
            moltenSkinDefInfo.UnlockableName = "MINER_MONSOONUNLOCKABLE_REWARD_ID";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerMolten").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 5);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerMolten").GetTexture("_EmissionMap"));

                array[0].defaultMaterial = material;
            }

            moltenSkinDefInfo.RendererInfos = array;

            SkinDef moltenSkin = LoadoutAPI.CreateNewSkinDef(moltenSkinDefInfo);

            LoadoutAPI.SkinDefInfo pupleSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            pupleSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            pupleSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            pupleSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            pupleSkinDefInfo.GameObjectActivations = getActivations(allObjects);
            pupleSkinDefInfo.Icon = Resources.Load<GameObject>("Prefabs/CharacterBodies/EngiBody").GetComponentInChildren<ModelSkinController>().skins[0].icon;
            pupleSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = mainRenderer.sharedMesh
                }
            };
            pupleSkinDefInfo.Name = "MINERBODY_PUPLE_SKIN_NAME";
            pupleSkinDefInfo.NameToken = "MINERBODY_PUPLE_SKIN_NAME";
            pupleSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            pupleSkinDefInfo.RootObject = model;
            pupleSkinDefInfo.UnlockableName = "";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerPuple").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 1);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerPuple").GetTexture("_EmissionMap"));

                array[0].defaultMaterial = material;
            }

            pupleSkinDefInfo.RendererInfos = array;

            SkinDef pupleSkin = LoadoutAPI.CreateNewSkinDef(pupleSkinDefInfo);

            LoadoutAPI.SkinDefInfo tundraSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            tundraSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            tundraSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            tundraSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            tundraSkinDefInfo.GameObjectActivations = getActivations(allObjects);
            tundraSkinDefInfo.Icon = Assets.mainAssetBundle.LoadAsset<Sprite>("texTundraAchievement");
            tundraSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = Assets.tundraMesh
                }
            };
            tundraSkinDefInfo.Name = "MINERBODY_TUNDRA_SKIN_NAME";
            tundraSkinDefInfo.NameToken = "MINERBODY_TUNDRA_SKIN_NAME";
            tundraSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            tundraSkinDefInfo.RootObject = model;
            tundraSkinDefInfo.UnlockableName = "MINER_TUNDRAUNLOCKABLE_REWARD_ID";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerTundra").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 1);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerTundra").GetTexture("_EmissionMap"));

                array[0].defaultMaterial = material;
            }

            tundraSkinDefInfo.RendererInfos = array;

            SkinDef tundraSkin = LoadoutAPI.CreateNewSkinDef(tundraSkinDefInfo);

            //minecraft-
            LoadoutAPI.SkinDefInfo ironSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            ironSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            ironSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            ironSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            ironSkinDefInfo.GameObjectActivations = getActivations(allObjects);
            ironSkinDefInfo.Icon = Resources.Load<GameObject>("Prefabs/CharacterBodies/HuntressBody").GetComponentInChildren<ModelSkinController>().skins[1].icon;
            ironSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = mainRenderer.sharedMesh
                }
            };
            ironSkinDefInfo.Name = "MINERBODY_IRON_SKIN_NAME";
            ironSkinDefInfo.NameToken = "MINERBODY_IRON_SKIN_NAME";
            ironSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            ironSkinDefInfo.RootObject = model;
            ironSkinDefInfo.UnlockableName = "";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerIron").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0);

                array[0].defaultMaterial = material;
            }

            ironSkinDefInfo.RendererInfos = array;

            SkinDef ironSkin = LoadoutAPI.CreateNewSkinDef(ironSkinDefInfo);

            LoadoutAPI.SkinDefInfo goldSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            goldSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            goldSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            goldSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            goldSkinDefInfo.GameObjectActivations = getActivations(allObjects);
            goldSkinDefInfo.Icon = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<ModelSkinController>().skins[1].icon;
            goldSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = mainRenderer.sharedMesh
                }
            };
            goldSkinDefInfo.Name = "MINERBODY_GOLD_SKIN_NAME";
            goldSkinDefInfo.NameToken = "MINERBODY_GOLD_SKIN_NAME";
            goldSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            goldSkinDefInfo.RootObject = model;
            goldSkinDefInfo.UnlockableName = "";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerGold").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0f);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerGold").GetTexture("_EmissionMap"));

                array[0].defaultMaterial = material;
            }

            goldSkinDefInfo.RendererInfos = array;

            SkinDef goldSkin = LoadoutAPI.CreateNewSkinDef(goldSkinDefInfo);

            LoadoutAPI.SkinDefInfo diamondSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            diamondSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            diamondSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            diamondSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            diamondSkinDefInfo.GameObjectActivations = getActivations(allObjects);
            diamondSkinDefInfo.Icon = Resources.Load<GameObject>("Prefabs/CharacterBodies/MercBody").GetComponentInChildren<ModelSkinController>().skins[0].icon;
            diamondSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = mainRenderer.sharedMesh
                }
            };
            diamondSkinDefInfo.Name = "MINERBODY_DIAMOND_SKIN_NAME";
            diamondSkinDefInfo.NameToken = "MINERBODY_DIAMOND_SKIN_NAME";
            diamondSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            diamondSkinDefInfo.RootObject = model;
            diamondSkinDefInfo.UnlockableName = "";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerDiamond").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0.75f);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerDiamond").GetTexture("_EmissionMap"));

                array[0].defaultMaterial = material;
            }

            diamondSkinDefInfo.RendererInfos = array;

            SkinDef diamondSkin = LoadoutAPI.CreateNewSkinDef(diamondSkinDefInfo);

            LoadoutAPI.SkinDefInfo steveSkinDefInfo = default(LoadoutAPI.SkinDefInfo);
            steveSkinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            steveSkinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            steveSkinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            steveSkinDefInfo.GameObjectActivations = getActivations(allObjects, diamondPickL, diamondPickR);
            steveSkinDefInfo.Icon = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<ModelSkinController>().skins[0].icon;
            steveSkinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    renderer = mainRenderer,
                    mesh = Assets.steveMesh
                }
            };
            steveSkinDefInfo.Name = "MINERBODY_STEVE_SKIN_NAME";
            steveSkinDefInfo.NameToken = "MINERBODY_STEVE_SKIN_NAME";
            steveSkinDefInfo.RendererInfos = characterModel.baseRendererInfos;
            steveSkinDefInfo.RootObject = model;
            steveSkinDefInfo.UnlockableName = "";

            rendererInfos = skinDefInfo.RendererInfos;
            array = new CharacterModel.RendererInfo[rendererInfos.Length];
            rendererInfos.CopyTo(array, 0);

            material = array[0].defaultMaterial;

            if (material)
            {
                material = UnityEngine.Object.Instantiate<Material>(material);
                material.SetTexture("_MainTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerSteve").GetTexture("_MainTex"));
                material.SetColor("_EmColor", Color.white);
                material.SetFloat("_EmPower", 0.75f);
                material.SetTexture("_EmTex", Assets.mainAssetBundle.LoadAsset<Material>("matMinerSteve").GetTexture("_EmissionMap"));

                array[0].defaultMaterial = material;
            }

            steveSkinDefInfo.RendererInfos = array;

            SkinDef steveSkin = LoadoutAPI.CreateNewSkinDef(steveSkinDefInfo);

            var skinDefs = new List<SkinDef>()
            {
                defaultSkin,
                moltenSkin,
                tundraSkin,
            };

            if (MinerPlugin.extraSkins.Value)
            {
                skinDefs.Add(pupleSkin);
                skinDefs.Add(ironSkin);
                skinDefs.Add(goldSkin);
                skinDefs.Add(diamondSkin);
                skinDefs.Add(steveSkin);
            }

            skinController.skins = skinDefs.ToArray();
        }
    }
}
