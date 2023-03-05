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
    internal const string LegendaryShieldAbility = "39db45ad-8f6f-4317-a0e6-6ae78e426d8e";

    internal const string BalancedArmor = "395f73da-d5a6-440c-a018-e9c0b83a92b6";
    internal const string BalancedArmorAbility = "fe735c67-37a2-412e-88cb-032ef9feacb8";
    internal const string BalancedArmorBuff = "a7d29359-91af-4b1f-af27-00e30f852d71";

    internal const string Bolstering = "32731c48-a494-48eb-a11e-d4725a808849";
    internal const string BolsteringTargetBuff = "d2022e07-3594-4563-b50b-bce30378c4f0";
    internal const string BolsteringAbility = "c32606ce-2792-48ae-9e20-026ae1ae4bf5";
    internal const string BolsteringBuff = "7e724a6b-54a2-4c93-bece-9bd18b3961dc";
    internal const string BolsteringShieldAbility = "345d4a07-ad19-4d38-98c6-14f959613342";
    internal const string BolsteringShieldBuff = "649dbf11-9213-409c-bf27-d833778621ba";

    internal const string Champion = "481c31bb-24ef-45be-aa53-55587f8e4377";
    internal const string ChampionAbility = "0cc47fcd-b173-4ad3-ae37-50e8d1b9abc5";
    internal const string ChampionBuff = "ff5e6c75-9b43-473c-a174-61663c709fb9";

    internal const string Dastard = "4abddf52-82b5-4d6a-a533-de8180ecfc79";
    internal const string DastardAbility = "bf642899-ae85-4f82-94d9-145970a4e11b";
    internal const string DastardBuff = "a21bd358-167e-4482-82ec-2362bfbfcb61";

    internal const string Deathless = "d44cfbde-4b53-4507-9dd2-e21b3ec48bab";
    internal const string DeathlessAbility = "adf4bdcb-3608-427e-b1f4-e59cc94e495a";
    internal const string DeathlessBuff = "a0750060-80f0-4652-b4fd-dd4bae5cca67";

    #region Defiant
    internal const string Defiant = "97592981-105e-4789-bdb7-af5316231987";

    internal const string DefiantParent = "1ad92549-0b42-4ea6-a3e0-33301026c0d9";
    internal const string DefiantShieldParent = "c7e450da-454f-4564-a673-27c27f52724c";

    internal const string DefiantAberrations = "192c32aa-5995-464c-8c5f-52298f8366f4";
    internal const string DefiantAberrationsAbility = "2e3e7c49-b64f-44fc-ad37-5b892387bf18";
    internal const string DefiantAberrationsBuff = "ba3eba6b-3fe1-479a-81b7-b222321e52dd";
    internal const string DefiantAberrationsShieldAbility = "32e8a12a-4abe-406f-adfe-f2b8440e82e4";
    internal const string DefiantAberrationsShieldBuff = "da005b7d-a2af-430f-ab7f-7bba8e69fff8";

    internal const string DefiantAnimals = "43374da6-5f08-4adf-bb7f-a6b3c8b24cbc";
    internal const string DefiantAnimalsAbility = "0e2ae4a6-3739-45da-9f19-da1cca55ef87";
    internal const string DefiantAnimalsBuff = "a15c9d9c-f900-47ce-8ecb-059b668659d3";
    internal const string DefiantAnimalsShieldAbility = "77d499c4-7534-40e0-b670-8fc67074e7ae";
    internal const string DefiantAnimalsShieldBuff = "025f0216-9c6e-4a64-bc32-25e1beebd051";

    internal const string DefiantConstructs = "2699b5ec-17e1-4503-b1af-c174103295a1";
    internal const string DefiantConstructsAbility = "bab8db40-1cc8-401f-bebb-a7eb8c076551";
    internal const string DefiantConstructsBuff = "3e5e8c87-c603-4498-b189-76b9d075089c";
    internal const string DefiantConstructsShieldAbility = "437bc337-711e-4e24-af00-aef8ab6401aa";
    internal const string DefiantConstructsShieldBuff = "0609550c-0f0c-4aea-a1a3-954cfbdb0fb3";

    internal const string DefiantDragons = "15e363fd-6b81-47e0-b7d6-c70847fadc56";
    internal const string DefiantDragonsAbility = "779f01a2-076f-4c95-ad90-5ea811767aa9";
    internal const string DefiantDragonsBuff = "68ef104d-bf90-48c6-aa5a-6f15e318fee5";
    internal const string DefiantDragonsShieldAbility = "b03f6759-66e9-4cc1-a1dc-1d6427cea8c7";
    internal const string DefiantDragonsShieldBuff = "88c61541-98a8-4a43-9a63-edaf9ea942dd";

    internal const string DefiantFey = "6fc07785-7e74-4b65-ad9f-0b2ad0f4619d";
    internal const string DefiantFeyAbility = "47410129-d369-4e67-89e3-39533853fef6";
    internal const string DefiantFeyBuff = "d801a36a-df08-4d41-829a-00541ccab9e8";
    internal const string DefiantFeyShieldAbility = "58c8388a-13f7-47cd-b836-3d3a9ee19155";
    internal const string DefiantFeyShieldBuff = "055863b2-1a9f-4c74-a320-5e46049541f1";

    internal const string DefiantHumanoidGiant = "bcda585d-bf64-4f33-adf2-ab140f95aa6b";
    internal const string DefiantHumanoidGiantAbility = "77994916-96c8-4630-a60e-16ffe1ff2a6d";
    internal const string DefiantHumanoidGiantBuff = "e2453e4f-384b-4332-8cfa-126960c991c5";
    internal const string DefiantHumanoidGiantShieldAbility = "e90db591-2eb0-41f5-b31e-f187fbadf257";
    internal const string DefiantHumanoidGiantShieldBuff = "a8ad1a79-34e2-4a25-b69e-6b012478acc6";

    internal const string DefiantHumanoidReptilian = "a92b6974-9ca1-43bd-8ad6-7e3578554723";
    internal const string DefiantHumanoidReptilianAbility = "f216cd02-1462-403c-b462-1774ae914d09";
    internal const string DefiantHumanoidReptilianBuff = "5f022777-fd7e-48fe-8f74-4230b63419f4";
    internal const string DefiantHumanoidReptilianShieldAbility = "95be3f59-4963-4419-9ca5-5e0f635b1822";
    internal const string DefiantHumanoidReptilianShieldBuff = "b7e6e592-2bed-498a-a0b8-f7259392c803";

    internal const string DefiantHumanoidMonstrous = "03628a54-2498-4d61-bd79-cf1156adedc3";
    internal const string DefiantHumanoidMonstrousAbility = "cc8cfdce-2e0a-4571-b8f9-7d8ca0c99042";
    internal const string DefiantHumanoidMonstrousBuff = "3feb2322-5ee5-42e3-97db-05352deb6af4";
    internal const string DefiantHumanoidMonstrousShieldAbility = "a2cd6855-018f-419c-b3a6-959a288ff098";
    internal const string DefiantHumanoidMonstrousShieldBuff = "ab5468f1-b134-4ba4-b8ac-5ef74816756e";

    internal const string DefiantMagicalBeasts = "d2b09fe0-2eb2-4a33-a095-7b48e5db64fc";
    internal const string DefiantMagicalBeastsAbility = "b8e89ae7-bf40-4bec-9a78-884dc7d6139c";
    internal const string DefiantMagicalBeastsBuff = "e2dd7276-49fd-476a-87b3-ebfd37fcc8b6";
    internal const string DefiantMagicalBeastsShieldAbility = "db3b4f4c-a434-4e1a-847b-b40c4a533ebc";
    internal const string DefiantMagicalBeastsShieldBuff = "c994ebcf-85b6-46e2-98c8-fd857aaf9f62";

    internal const string DefiantOutsiderGood = "365053c4-672b-4706-909c-39d1df92f267";
    internal const string DefiantOutsiderGoodAbility = "e4a900c3-5ac8-4ef9-8954-95ea15417569";
    internal const string DefiantOutsiderGoodBuff = "a02c7c21-3e2b-422a-a222-12ddd1f7de11";
    internal const string DefiantOutsiderGoodShieldAbility = "fb8cb5f2-1d8a-48d0-a0e5-a4bec09e3097";
    internal const string DefiantOutsiderGoodShieldBuff = "e1ee5b41-2ac2-45b9-95a1-70773783ffe6";

    internal const string DefiantOutsiderEvil = "e7b9f37a-ac9e-4cf3-9f7a-007e2ec416da";
    internal const string DefiantOutsiderEvilAbility = "255ed55d-4cd7-48bd-8cc2-6799b999e4c5";
    internal const string DefiantOutsiderEvilBuff = "ae263068-dfd9-45e9-9bcb-358e180486ea";
    internal const string DefiantOutsiderEvilShieldAbility = "8122b83d-8081-4eb3-91b9-c2dc5b911608";
    internal const string DefiantOutsiderEvilShieldBuff = "b96bde35-e59c-4271-b719-711d788f565f";

    internal const string DefiantOutsiderLawful = "223f9cdf-0f58-4ef3-bdae-cfcf0d929460";
    internal const string DefiantOutsiderLawfulAbility = "965247c6-ad1e-455e-8b85-6861d7d60c4b";
    internal const string DefiantOutsiderLawfulBuff = "4cc5dc5c-186d-4de1-9818-8e7792600368";
    internal const string DefiantOutsiderLawfulShieldAbility = "eac33e1b-8427-4bbe-beaa-d543e920387c";
    internal const string DefiantOutsiderLawfulShieldBuff = "0d1e0971-f9d5-4200-b6f2-5c6c46ec6f16";

    internal const string DefiantOutsiderChaotic = "75bc18f1-81ad-464c-94de-b2d4a94aca58";
    internal const string DefiantOutsiderChaoticAbility = "572ca8b5-301b-4a29-9e55-4a06c1a82639";
    internal const string DefiantOutsiderChaoticBuff = "09fdab88-6744-4d72-a38b-4419e527b9c3";
    internal const string DefiantOutsiderChaoticShieldAbility = "12adfa84-f7fc-4ebf-bc6f-d86c7d605c42";
    internal const string DefiantOutsiderChaoticShieldBuff = "867036b8-aaba-42b6-9425-4fca7132370a";

    internal const string DefiantOutsiderNeutral = "1c052f78-bb2e-4783-a3a9-06ecada8915b";
    internal const string DefiantOutsiderNeutralAbility = "739df56f-fb75-4597-a7ab-5f7cfd04a429";
    internal const string DefiantOutsiderNeutralBuff = "d8895833-86cb-4883-af3d-ba19d2867887";
    internal const string DefiantOutsiderNeutralShieldAbility = "40db8125-92b5-4328-9c3e-5318becf00b2";
    internal const string DefiantOutsiderNeutralShieldBuff = "9c2d748d-6c18-4cd6-96db-d21576e3aed7";

    internal const string DefiantPlants = "31890128-fbc4-4994-b0ba-2a8ba8f7b12f";
    internal const string DefiantPlantsAbility = "5683447f-8ebe-445b-ab03-5952ec548833";
    internal const string DefiantPlantsBuff = "3b2f059b-3233-4dee-a188-ec06f89a791c";
    internal const string DefiantPlantsShieldAbility = "351da87d-3cd4-48b6-ac5c-5ccaa96cdf4f";
    internal const string DefiantPlantsShieldBuff = "bcb894fb-4dc2-477e-8d74-8fe3f64ca2e5";

    internal const string DefiantUndead = "598998fe-1999-4670-b184-fce2661dda81";
    internal const string DefiantUndeadAbility = "542779a9-640f-4c75-8772-8b45a49a0766";
    internal const string DefiantUndeadBuff = "a767aa99-3c8f-4533-a226-4ae5b2f45afd";
    internal const string DefiantUndeadShieldAbility = "9fdd271b-7f20-45da-a08f-354a544ccdfb";
    internal const string DefiantUndeadShieldBuff = "d7b3b11a-7ae7-4e55-aa9c-fecca4eb04fa";

    internal const string DefiantVermin = "b4508f69-8316-47ee-ac3c-1b9ea8979103";
    internal const string DefiantVerminAbility = "c6e8654f-d38d-40fe-9a2b-3fc8aeb11d96";
    internal const string DefiantVerminBuff = "cd0fa7ea-3954-4f54-8dd6-c1a502d9fc32";
    internal const string DefiantVerminShieldAbility = "1432922d-17d6-4745-8b52-a817f1896976";
    internal const string DefiantVerminShieldBuff = "dc684853-855e-48bb-8b19-354509c157ce";
    #endregion

    #region Energy Resistance
    internal const string EnergyResistParent = "295f255f-0a3c-42f9-b8a0-2f6d80f9dc16";
    internal const string EnergyResistShieldParent = "8721d4dc-30f8-4ec9-847b-23854bae0d22";

    internal const string EnergyResist10 = "9fdc3494-2df3-4584-b732-646b4655ab0b";
    internal const string AcidResist10Ability = "459f021a-ff77-410f-8e24-32d541505961";
    internal const string AcidResist10Buff = "737b2b79-63d6-4677-ab4b-57e1c6d4c0a6";
    internal const string AcidResist10ShieldAbility = "d7e0c101-69b8-4915-b4ec-e949bd8088e0";
    internal const string AcidResist10ShieldBuff = "060761d3-7975-44b0-a8ac-9da29df413c0";
    internal const string ColdResist10Ability = "cec7be47-fa90-4664-8a84-9a1730b1ca09";
    internal const string ColdResist10Buff = "6e3b9b46-ed38-4cb7-9083-54e427b2deee";
    internal const string ColdResist10ShieldAbility = "eace679d-870b-46aa-a55f-39745b9a8171";
    internal const string ColdResist10ShieldBuff = "521f1397-27dc-4dea-b780-de09ef5f0abf";
    internal const string ElectricityResist10Ability = "b57b6d8d-cf93-4580-8d0e-85e645433914";
    internal const string ElectricityResist10Buff = "5b78e287-397c-45c7-8b02-8a74c1db6081";
    internal const string ElectricityResist10ShieldAbility = "98faed91-3f3a-4c67-9e71-5c293eda1060";
    internal const string ElectricityResist10ShieldBuff = "bb2fb8a9-a420-481b-89f2-24fb5a4f405a";
    internal const string FireResist10Ability = "f18fd49d-943f-461c-b274-4e6f1f21efa0";
    internal const string FireResist10Buff = "a8cf1622-79a9-45c2-9429-51a963f29406";
    internal const string FireResist10ShieldAbility = "1c30ebe0-7e82-4f75-b74e-d22d73957f5b";
    internal const string FireResist10ShieldBuff = "15f74b99-8c1c-4485-8925-cd506b869c3d";
    internal const string SonicResist10Ability = "1e2d6827-fe4d-4c58-acb2-cedf71255ecf";
    internal const string SonicResist10Buff = "36d2ec4d-d1bd-4dfa-aee0-e18f0ca338b6";
    internal const string SonicResist10ShieldAbility = "074b2860-a764-4be2-a7f1-623b8922f418";
    internal const string SonicResist10ShieldBuff = "a995f85d-ee8a-4c9b-887c-efa0cce8865f";

    internal const string EnergyResist20 = "a105f2d7-7338-4299-824a-a01e6ccc7e31";
    internal const string AcidResist20Ability = "4a61cdfc-1e55-4a39-8022-6826b7edc113";
    internal const string AcidResist20Buff = "0efd352a-b3d2-41e7-9cd6-7b8a84493c08";
    internal const string AcidResist20ShieldAbility = "a507a789-5e7b-468f-8399-7af7c7012924";
    internal const string AcidResist20ShieldBuff = "02f8fd02-2dc1-442c-8c35-6634ee6f0b33";
    internal const string ColdResist20Ability = "34518a03-6ccb-43ac-9bea-9a4aafe3b9cc";
    internal const string ColdResist20Buff = "77502710-6b75-4db7-811d-f7a903e5b109";
    internal const string ColdResist20ShieldAbility = "30a8cd37-93fb-4df7-b8b7-3c24cd46d398";
    internal const string ColdResist20ShieldBuff = "d86e95a7-60db-4860-b5ea-5ea74bca13ff";
    internal const string ElectricityResist20Ability = "9f598605-ec89-44d5-8194-bd72da9c1cfe";
    internal const string ElectricityResist20Buff = "4ad93d3c-1632-464a-989f-2be2d5dd4354";
    internal const string ElectricityResist20ShieldAbility = "285bd44e-96ee-495c-96b3-3c2e399192d7";
    internal const string ElectricityResist20ShieldBuff = "da176901-dba0-499f-a702-8c2feb00570c";
    internal const string FireResist20Ability = "8c94339b-2fbf-4fd0-bff5-1626905d8423";
    internal const string FireResist20Buff = "cebaa178-8457-4e76-a2d8-52865ad55020";
    internal const string FireResist20ShieldAbility = "88613ebf-8f14-4b48-a3eb-072ca73a5cbd";
    internal const string FireResist20ShieldBuff = "f41f3a61-699f-42d5-b8b0-bac37f34e3dd";
    internal const string SonicResist20Ability = "b3a09bb3-b5df-490d-ad67-8b63ea5e6439";
    internal const string SonicResist20Buff = "e36f3f8c-a81d-4de4-b0b2-4eb039f73f5a";
    internal const string SonicResist20ShieldAbility = "559eac47-85a5-4b1e-82fa-2cb9f3a405cd";
    internal const string SonicResist20ShieldBuff = "d1fe93c4-5b9a-4c66-95a7-8500268a2da0";

    internal const string EnergyResist30 = "3df00008-64c7-46e2-a5ea-b69de1f98bf6";
    internal const string AcidResist30Ability = "af71eebc-bfb3-4d5a-bed9-10cf81ca2382";
    internal const string AcidResist30Buff = "50fc2507-045b-4ae1-87c6-c5f5e19524a0";
    internal const string AcidResist30ShieldAbility = "5528f26a-f5f0-47b8-b5c4-0e739b7f6b6a";
    internal const string AcidResist30ShieldBuff = "c19ba6f4-342c-401d-8d29-1fd48e2cd500";
    internal const string ColdResist30Ability = "fbacbbb6-f776-46ce-bf23-a4e2498d2a20";
    internal const string ColdResist30Buff = "c6dbd36b-e791-48ae-8774-cef122b0107d";
    internal const string ColdResist30ShieldAbility = "14e05d4b-417a-4507-8e58-94278b28e2c7";
    internal const string ColdResist30ShieldBuff = "2693383c-2935-470f-bbb1-d0632585158a";
    internal const string ElectricityResist30Ability = "2b3eb230-055c-4aea-9f85-9a8af61221e1";
    internal const string ElectricityResist30Buff = "50ebb877-881b-45ec-97de-2a22f635ed0f";
    internal const string ElectricityResist30ShieldAbility = "c4e45d10-fe62-4819-8272-ae04b7221a19";
    internal const string ElectricityResist30ShieldBuff = "53314f1b-26bf-4ffb-bc50-617f00a1bfec";
    internal const string FireResist30Ability = "5ca47d6d-4c04-407e-97c4-0dff35d394f3";
    internal const string FireResist30Buff = "cbf5f8a2-5bb1-419b-9fbe-bac804dea9a5";
    internal const string FireResist30ShieldAbility = "0339a690-2b76-4572-b92f-4a08cca254cf";
    internal const string FireResist30ShieldBuff = "3a88e1c8-643f-49dd-92aa-3b028f6dd783";
    internal const string SonicResist30Ability = "e0736dad-d2e3-40be-b88b-57f225fe3e12";
    internal const string SonicResist30Buff = "15c76fa0-08f9-4dae-a6a3-adc26551d1d7";
    internal const string SonicResist30ShieldAbility = "f962d547-dd1c-4416-9781-49eb2657bad8";
    internal const string SonicResist30ShieldBuff = "f9a9cacc-ab26-4d27-b587-f2aab4d45777";
    #endregion

    internal const string Fortification = "8c0470a2-a1fa-44f9-8d42-a46faf3145c1";
    internal const string FortificationAbility = "a5f06b4c-8070-48ca-afb3-0f7d377a0ab1";
    internal const string FortificationBuff = "b8b39cb6-7ce3-4a10-ab24-0f773e22f9b2";
    internal const string FortificationShieldAbility = "3d3df8d8-635e-47dc-86ac-848b44505089";
    internal const string FortificationShieldBuff = "035bf5bc-4180-4163-a2f7-377a1e12b9e6";

    internal const string ImprovedFortification = "450ca6b5-6951-4e76-ab21-fb9f62fc901b";
    internal const string ImprovedFortificationAbility = "825fa562-9df4-4e1a-aea7-9499ed2d2e54";
    internal const string ImprovedFortificationBuff = "bb04409f-824e-4c0b-b3ab-457bbfebf38d";
    internal const string ImprovedFortificationShieldAbility = "ff285961-c89e-44e0-ad8a-f4c9fb2308ee";
    internal const string ImprovedFortificationShieldBuff = "9e471109-647b-427b-9aa0-858518489952";

    internal const string GreaterFortification = "73f6c79d-9e1e-49bd-a097-b0beaa1ab4c0";
    internal const string GreaterFortificationAbility = "c80eb265-b9d0-425b-a94e-da01b52b10ed";
    internal const string GreaterFortificationBuff = "a5fbdedb-4d43-4b5a-8fe3-016e12dd90ed";
    internal const string GreaterFortificationShieldAbility = "622fe986-11c1-478c-ba04-dd4bbb366c11";
    internal const string GreaterFortificationShieldBuff = "c0dd6ab0-d52c-4772-8811-1d4aa422e550";

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
    internal const string SpellResistance13ShieldAbility = "2ca4f04e-90ae-4796-86e3-ee22c46c787f";
    internal const string SpellResistance13ShieldBuff = "e81c3f44-0ad2-40a1-b0d1-d3fea8358039";

    internal const string SpellResistance16 = "00c6c4f2-72e1-4be4-8f3f-9c501f71ee44";
    internal const string SpellResistance16Ability = "7accba64-307e-424b-8524-3ec0a4850dca";
    internal const string SpellResistance16Buff = "0a254e25-aa90-4c6c-a0bc-0950eb00e923";
    internal const string SpellResistance16ShieldAbility = "74640f8f-8b00-4bc3-8c7a-4007736b285e";
    internal const string SpellResistance16ShieldBuff = "be265426-e20b-4e3b-88b5-c04211adb69e";

    internal const string SpellResistance19 = "b74f7f6d-b20b-4168-b648-edc11051c349";
    internal const string SpellResistance19Ability = "e5fffcc3-068e-4f4c-a8cc-e148c4309866";
    internal const string SpellResistance19Buff = "ad53e752-146a-484c-a8e4-53eeff4c62f9";
    internal const string SpellResistance19ShieldAbility = "39c70810-a246-4c88-98d4-fc305d762207";
    internal const string SpellResistance19ShieldBuff = "0c9b59d7-bf67-45ec-aca1-5cf21efc5da8";

    internal const string SpellResistance22 = "8771a086-5fe3-41ee-a917-761e7982a0fd";
    internal const string SpellResistance22Ability = "f80b78e1-c450-404c-b553-1fe1abf758cd";
    internal const string SpellResistance22Buff = "70b71910-1c40-476a-84d6-cd838ced2260";
    internal const string SpellResistance22ShieldAbility = "d5cfab52-86ee-44ed-ac85-3c2e8fffe79e";
    internal const string SpellResistance22ShieldBuff = "15c1bbf0-db26-4a92-b309-345f8159395c";
    #endregion
    #endregion

    #region From Mods
    // Expanded Content - Anti-Paladin Smite
    internal const string SinfulAbsolutionBuff = "B13960D1-559A-414D-819B-FA53CF8526D4";

    // Microscopic Content Expansion - Anti-Paladin Smite
    internal const string SmiteGoodBuff = "7ff9e143-dd21-4641-a7f5-3ee10d7e98b5";
    #endregion
  }
}