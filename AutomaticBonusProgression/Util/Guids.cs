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
    #endregion
  }
}