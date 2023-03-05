namespace AutomaticBonusProgression.Util
{
  /// <summary>
  /// List of new and mod Guids.
  /// </summary>
  internal class Guids
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Guids));

    #region Attunement
    // Feature used to calculate armor / weapon / shield enhancement bonuses
    internal const string EnhancementCalculator = "945f05cf-ffb5-489d-b4d8-2687d1f1b835";

    internal const string ArmorAttunement = "24af67c6-bc58-4dba-a9bf-d7d00925e7c7";
    internal const string ShieldAttunement = "58b87d99-125c-4300-9c87-ba0d90168ca2";
    internal const string LegendaryArmorAttunement = "8b361c98-2aa0-4109-9a11-33a9fc997793";

    internal const string WeaponAttunement = "137eeed7-8487-411c-b861-15ded694bd08";
    internal const string OffHandAttunement = "84fedf0f-50fa-4b26-8e61-5c2e280bfc8e";
    internal const string LegendaryWeaponAttunement = "066c8db2-569e-47bd-a912-c13407efc0a4";

    internal const string Deflection = "b9af42db-c718-42c5-89e1-5376619e2460";
    internal const string Toughening = "0de33c56-4913-4b69-b1d0-619cd663beff";
    internal const string Resistance = "a5011356-2852-40ee-9841-d8bbf58d6336";

    #region Mental Prowess
    internal const string IntPlus2 = "ea260889-0874-45aa-9e79-4351dfc626e8";
    internal const string IntPlus4 = "2319a7bb-7c7b-4f50-8693-4124a7bc50cb";
    internal const string IntPlus6 = "f4da9b75-b1c8-4f70-9f28-e3b70d58927f";

    internal const string WisPlus2 = "927f8f04-9ad3-4b5b-a789-dc4dd5cb00f8";
    internal const string WisPlus4 = "dff83a15-66b5-4773-8f06-e8763d320973";
    internal const string WisPlus6 = "a25b60b3-bf51-4dc2-81de-abe48504b2ac";

    internal const string ChaPlus2 = "a2b5f61a-8468-43bb-a3e7-87908fe45f9c";
    internal const string ChaPlus4 = "cfed1413-c108-4b79-9aea-23202f2e18f2";
    internal const string ChaPlus6 = "e5e9e4dc-c772-46b2-9473-3786b92d837f";

    internal const string MentalProwessPrimarySelection = "e7df3b10-578f-439d-8976-657af9b68965";
    internal const string IntPrimaryProgression = "42918774-3271-48be-82d4-feb61fd6ae71";
    internal const string WisPrimaryProgression = "23f442fa-6dd6-43d9-84d3-3892122c19a3";
    internal const string ChaPrimaryProgression = "2554fcf0-4fda-438d-9e1d-020d8f083f86";

    internal const string MentalProwessSecondarySelection = "f8b980bc-3d95-4762-8748-2d26a00448b8";
    internal const string IntSecondaryProgression = "056eedf4-82fb-43bf-88ba-2fa31615f342";
    internal const string WisSecondaryProgression = "c64e6915-24e1-436e-95b4-143ce9b900ca";
    internal const string ChaSecondaryProgression = "3222aef3-13b9-4ef8-a137-e7444ceb661f";

    internal const string MentalProwessTertiarySelection = "2633b3b8-d88f-475e-8c07-7815b254f9f4";
    internal const string IntTertiaryProgression = "635c31cb-5e84-4bca-b107-33289ed6dbf9";
    internal const string WisTertiaryProgression = "3041bf37-6404-4b75-8b1a-c6d245ed1fed";
    internal const string ChaTertiaryProgression = "e00691ed-d52d-4b3d-b105-8898285292da";

    internal const string MentalProwessAnySelection = "9ba9c639-1a58-4006-bce2-093291a453fa";
    #endregion

    #region Physical Prowess
    internal const string StrPlus2 = "7d2b1e99-808c-455f-977a-547eca0bbfc3";
    internal const string StrPlus4 = "af64e128-c1e4-4d85-a454-450e3dfb31ee";
    internal const string StrPlus6 = "cb9fdd25-2abe-46cf-86ed-b79dbcd1dd6b";

    internal const string DexPlus2 = "f400157d-d8a8-4b2a-9daa-5866a5f61dfd";
    internal const string DexPlus4 = "f066810c-e6e1-4b4e-89f1-4e316e6e7db3";
    internal const string DexPlus6 = "c8b46c21-24d5-436b-b883-20c4f7bcbcdd";

    internal const string ConPlus2 = "2ca04f63-07ef-4ed5-9796-3ad1e4b04b55";
    internal const string ConPlus4 = "6b171aae-37a7-444e-834e-070cec520fd3";
    internal const string ConPlus6 = "5bfbeec0-f39b-46bb-accf-afc278e19b5d";

    internal const string PhysicalProwessPrimarySelection = "25c94c69-c2e1-4ccc-a3e5-0fe6c9f1bb05";
    internal const string StrPrimaryProgression = "503f80de-37f9-4f69-a51f-ac268de02867";
    internal const string DexPrimaryProgression = "6554309c-139d-4995-825d-ddcee606e45e";
    internal const string ConPrimaryProgression = "db7ab3fe-b416-46ee-8307-fd57aa790c99";

    internal const string PhysicalProwessSecondarySelection = "7ace0142-8de7-405e-b448-d611fc9021df";
    internal const string StrSecondaryProgression = "2dd700c1-b662-4e17-8810-6d70404e4eab";
    internal const string DexSecondaryProgression = "6778522c-fa43-4826-bc56-97d513956930";
    internal const string ConSecondaryProgression = "df44c4ef-02da-4313-9513-615bdc3fd5e9";

    internal const string PhysicalProwessTertiarySelection = "a8e85712-6d34-422f-ad15-3a00bb297255";
    internal const string StrTertiaryProgression = "a872b564-74aa-48ee-9cf0-887d0089db57";
    internal const string DexTertiaryProgression = "53057a69-f6f4-4c54-b2d5-353a2e6e62be";
    internal const string ConTertiaryProgression = "a318821f-5d07-4d8c-b1c3-8210cb267848";

    internal const string PhysicalProwessAnySelection = "da988f82-a5d3-47e3-a134-e0abc8976a50";
    #endregion
    #endregion

    #region Legendary Gifts
    internal const string LegendaryGifts = "bbf17fd7-27b1-43a7-be09-6cb9c2afc546";

    internal const string LegendaryAbility = "0e44687f-ba76-4a2b-9ebd-1fee30ca06db";
    internal const string LegendaryStrength = "b8c3a13e-ba3e-47a5-a1fa-f2016d0fb682";
    internal const string LegendaryDexterity = "64cd8ffd-134a-4e63-8e7e-1a6ba328a6b0";
    internal const string LegendaryConstitution = "a8aab8e0-e601-4b71-82db-ecd5dfdb5953";
    internal const string LegendaryIntelligence = "49fc30e1-bd73-413d-948c-c47493d310db";
    internal const string LegendaryWisdom = "e2411919-40ff-4acf-bb75-0f5a27ad6c88";
    internal const string LegendaryCharisma = "95483f02-93c5-4301-99e5-38e8ea2ca3bb";

    internal const string LegendaryShieldmaster = "baef229b-a7fa-4618-88ed-23f9e6875d18";
    internal const string LegendaryTwinWeapons = "d910642b-7719-4607-b6e0-0d614f1a30e0";

    internal const string LegendaryProwess = "4af6f190-145c-473e-a584-bcc0143e838d";

    #region Legendary Armor
    internal const string LegendaryArmor = "171db362-874c-468a-8626-b3e2c9deb1d4";
    internal const string LegendaryArmorAbility = "b1c038e4-f34f-443b-b080-23d44cfba390";
    internal const string LegendaryShield = "8db1c050-f274-4c51-a0b1-55f396b1501b";
    internal const string LegendaryShieldAbility = "39db45ad-8f6f-4317-a0e6-6ae78e426d8e";

    internal const string BalancedArmor = "395f73da-d5a6-440c-a018-e9c0b83a92b6";
    internal const string BalancedArmorAbility = "fe735c67-37a2-412e-88cb-032ef9feacb8";
    internal const string BalancedArmorBuff = "a7d29359-91af-4b1f-af27-00e30f852d71";

    internal const string Fortification = "8c0470a2-a1fa-44f9-8d42-a46faf3145c1";
    internal const string FortificationAbility = "a5f06b4c-8070-48ca-afb3-0f7d377a0ab1";
    internal const string FortificationBuff = "b8b39cb6-7ce3-4a10-ab24-0f773e22f9b2";

    internal const string EnergyResist10 = "9fdc3494-2df3-4584-b732-646b4655ab0b";
    internal const string AcidResist10Ability = "459f021a-ff77-410f-8e24-32d541505961";
    internal const string AcidResist10Buff = "737b2b79-63d6-4677-ab4b-57e1c6d4c0a6";
    internal const string ColdResist10Ability = "cec7be47-fa90-4664-8a84-9a1730b1ca09";
    internal const string ColdResist10Buff = "6e3b9b46-ed38-4cb7-9083-54e427b2deee";
    internal const string ElectricityResist10Ability = "b57b6d8d-cf93-4580-8d0e-85e645433914";
    internal const string ElectricityResist10Buff = "5b78e287-397c-45c7-8b02-8a74c1db6081";
    internal const string FireResist10Ability = "f18fd49d-943f-461c-b274-4e6f1f21efa0";
    internal const string FireResist10Buff = "a8cf1622-79a9-45c2-9429-51a963f29406";
    internal const string SonicResist10Ability = "1e2d6827-fe4d-4c58-acb2-cedf71255ecf";
    internal const string SonicResist10Buff = "36d2ec4d-d1bd-4dfa-aee0-e18f0ca338b6";

    internal const string EnergyResist20 = "a105f2d7-7338-4299-824a-a01e6ccc7e31";
    internal const string AcidResist20Ability = "4a61cdfc-1e55-4a39-8022-6826b7edc113";
    internal const string AcidResist20Buff = "0efd352a-b3d2-41e7-9cd6-7b8a84493c08";
    internal const string ColdResist20Ability = "34518a03-6ccb-43ac-9bea-9a4aafe3b9cc";
    internal const string ColdResist20Buff = "77502710-6b75-4db7-811d-f7a903e5b109";
    internal const string ElectricityResist20Ability = "9f598605-ec89-44d5-8194-bd72da9c1cfe";
    internal const string ElectricityResist20Buff = "4ad93d3c-1632-464a-989f-2be2d5dd4354";
    internal const string FireResist20Ability = "8c94339b-2fbf-4fd0-bff5-1626905d8423";
    internal const string FireResist20Buff = "cebaa178-8457-4e76-a2d8-52865ad55020";
    internal const string SonicResist20Ability = "b3a09bb3-b5df-490d-ad67-8b63ea5e6439";
    internal const string SonicResist20Buff = "e36f3f8c-a81d-4de4-b0b2-4eb039f73f5a";

    internal const string EnergyResist30 = "3df00008-64c7-46e2-a5ea-b69de1f98bf6";
    internal const string AcidResist30Ability = "af71eebc-bfb3-4d5a-bed9-10cf81ca2382";
    internal const string AcidResist30Buff = "50fc2507-045b-4ae1-87c6-c5f5e19524a0";
    internal const string ColdResist30Ability = "fbacbbb6-f776-46ce-bf23-a4e2498d2a20";
    internal const string ColdResist30Buff = "c6dbd36b-e791-48ae-8774-cef122b0107d";
    internal const string ElectricityResist30Ability = "2b3eb230-055c-4aea-9f85-9a8af61221e1";
    internal const string ElectricityResist30Buff = "50ebb877-881b-45ec-97de-2a22f635ed0f";
    internal const string FireResist30Ability = "5ca47d6d-4c04-407e-97c4-0dff35d394f3";
    internal const string FireResist30Buff = "cbf5f8a2-5bb1-419b-9fbe-bac804dea9a5";
    internal const string SonicResist30Ability = "e0736dad-d2e3-40be-b88b-57f225fe3e12";
    internal const string SonicResist30Buff = "15c76fa0-08f9-4dae-a6a3-adc26551d1d7";

    internal const string ImprovedFortification = "450ca6b5-6951-4e76-ab21-fb9f62fc901b";
    internal const string ImprovedFortificationAbility = "825fa562-9df4-4e1a-aea7-9499ed2d2e54";
    internal const string ImprovedFortificationBuff = "bb04409f-824e-4c0b-b3ab-457bbfebf38d";

    internal const string GreaterFortification = "73f6c79d-9e1e-49bd-a097-b0beaa1ab4c0";
    internal const string GreaterFortificationAbility = "c80eb265-b9d0-425b-a94e-da01b52b10ed";
    internal const string GreaterFortificationBuff = "a5fbdedb-4d43-4b5a-8fe3-016e12dd90ed";

    internal const string Invulnerability = "03d89904-8e99-4856-8c9c-719e77463141";
    internal const string InvulnerabilityAbility = "bc659b72-52c3-4414-9c10-9d67e533d12d";
    internal const string InvulnerabilityBuff = "d77e2446-276a-4246-924e-d679b526eea8";

    internal const string ShadowArmor = "86580aff-6433-4b70-823a-8d4d8d904f46";
    internal const string ShadowArmorAbility = "f0096427-0ec6-4e72-b21b-e5251d39bed6";
    internal const string ShadowArmorBuff = "1cb18f84-4ae0-4990-930f-f47f42ff549d";

    internal const string ImprovedShadowArmor = "c06851ca-0d5e-45ae-a18d-de485622e4f1";
    internal const string ImprovedShadowArmorAbility = "28e986dc-049d-461a-a170-3b267df847ba";
    internal const string ImprovedShadowArmorBuff = "8026ac99-35ce-43c2-8e0b-f076530882f8";

    internal const string GreaterShadowArmor = "090411ba-8a36-4077-a558-b8d3e497f09f";
    internal const string GreaterShadowArmorAbility = "1af9720f-872a-4780-86ef-32c7a49e8e10";
    internal const string GreaterShadowArmorBuff = "3b48de35-4127-474e-a3bb-020329815de4";

    internal const string SpellResistance13 = "e60ba111-956c-437d-8491-b4f4e4ecadb7";
    internal const string SpellResistance13Ability = "11b0f11b-6bc5-46a1-83dd-cf13ee416d4e";
    internal const string SpellResistance13Buff = "d96fab7a-c514-42ff-b336-a27a8e6e7730";

    internal const string SpellResistance16 = "00c6c4f2-72e1-4be4-8f3f-9c501f71ee44";
    internal const string SpellResistance16Ability = "7accba64-307e-424b-8524-3ec0a4850dca";
    internal const string SpellResistance16Buff = "0a254e25-aa90-4c6c-a0bc-0950eb00e923";

    internal const string SpellResistance19 = "b74f7f6d-b20b-4168-b648-edc11051c349";
    internal const string SpellResistance19Ability = "e5fffcc3-068e-4f4c-a8cc-e148c4309866";
    internal const string SpellResistance19Buff = "ad53e752-146a-484c-a8e4-53eeff4c62f9";

    internal const string SpellResistance22 = "8771a086-5fe3-41ee-a917-761e7982a0fd";
    internal const string SpellResistance22Ability = "f80b78e1-c450-404c-b553-1fe1abf758cd";
    internal const string SpellResistance22Buff = "70b71910-1c40-476a-84d6-cd838ced2260";
    #endregion
    #endregion
  }
}