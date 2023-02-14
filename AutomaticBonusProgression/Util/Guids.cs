namespace AutomaticBonusProgression.Util
{
  /// <summary>
  /// List of new and mod Guids.
  /// </summary>
  internal class Guids
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Guids));

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

    internal const string IntBonus = "ea260889-0874-45aa-9e79-4351dfc626e8";
    internal const string WisBonus = "927f8f04-9ad3-4b5b-a789-dc4dd5cb00f8";
    internal const string ChaBonus = "a2b5f61a-8468-43bb-a3e7-87908fe45f9c";

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
    internal const string IntAny = "2319a7bb-7c7b-4f50-8693-4124a7bc50cb";
    internal const string WisAny = "dff83a15-66b5-4773-8f06-e8763d320973";
    internal const string ChaAny = "cfed1413-c108-4b79-9aea-23202f2e18f2";
  }
}
