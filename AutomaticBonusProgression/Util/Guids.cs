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

    internal const string Bashing = "01a0061e-5fe1-40a1-b870-84dbfacf04de";
    internal const string BashingAbility = "a0e94cc2-8004-4803-9f81-e94f446f31b3";
    internal const string BashingBuff = "d3db2785-81b5-4a9f-aa1e-e91dc8b1bd95";

    internal const string Blinding = "72a010f1-2f82-4361-b591-d57bf448cecf";
    internal const string BlindingAbility = "b4b6ff8b-41b9-4c92-a13e-d510f0c0e400";
    internal const string BlindingBuff = "7b6a19ad-2a0e-4845-8ff7-33a656a97825";
    internal const string BlindingCastAbility = "d7305e2a-3b76-480e-96e7-4db4515407cd";
    internal const string BlindingCastResource = "6b9e185f-9e53-4344-9df1-46df15ff8c74";

    internal const string Bolstering = "32731c48-a494-48eb-a11e-d4725a808849";
    internal const string BolsteringTargetBuff = "d2022e07-3594-4563-b50b-bce30378c4f0";
    internal const string BolsteringAbility = "c32606ce-2792-48ae-9e20-026ae1ae4bf5";
    internal const string BolsteringBuff = "7e724a6b-54a2-4c93-bece-9bd18b3961dc";
    internal const string BolsteringShieldAbility = "345d4a07-ad19-4d38-98c6-14f959613342";
    internal const string BolsteringShieldBuff = "649dbf11-9213-409c-bf27-d833778621ba";

    internal const string Brawling = "10fc122a-2307-4f34-993b-63ae6b6bdc47";
    internal const string BrawlingAbility = "6cb01292-69b5-4f13-81fb-c62a5f1b6f6d";
    internal const string BrawlingBuff = "3668e7c1-3ebf-4fd9-a6df-4d1768259649";

    internal const string Champion = "481c31bb-24ef-45be-aa53-55587f8e4377";
    internal const string ChampionAbility = "0cc47fcd-b173-4ad3-ae37-50e8d1b9abc5";
    internal const string ChampionBuff = "ff5e6c75-9b43-473c-a174-61663c709fb9";

    internal const string Creeping = "d6a3ac53-2012-4444-adc6-4fa9194af62a";
    internal const string CreepingAbility = "d5b06e1d-8278-4ce4-a3e2-f7540b179390";
    internal const string CreepingBuff = "87eaad30-abdc-4cc1-8bea-46ca587e77d7";
    internal const string CreepingCastAbility = "a233ebd4-a677-4100-b417-209bf5daafb6";
    internal const string CreepingCastBuff = "95ab4589-fcb6-4254-a0fe-e09d2b4a78b2";
    internal const string CreepingCastResource = "ecd8026d-4884-4108-bf86-67bf58456f85";

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

    internal const string Determination = "76df287a-eb8b-46e4-aefb-b0b8bdffb6e2";
    internal const string DeterminationAbility = "9f592dd8-a746-4b11-b753-6da7395e8b76";
    internal const string DeterminationBuff = "0be08da1-8f39-459e-a6e8-89eead23a135";
    internal const string DeterminationResource = "45e5d3b4-3c7d-4156-986f-aa731cf95811";

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

    internal const string Expeditious = "31136772-b598-4af7-a752-f22c48975df1";
    internal const string ExpeditiousAbility = "9b28ffe1-e85b-439e-9e50-e7d334be9151";
    internal const string ExpeditiousBuff = "c737fc3f-d4ee-485b-97b9-d4255cca521a";
    internal const string ExpeditiousCastAbility = "9d94a4f9-88b1-49d4-9379-f88ffce54478";
    internal const string ExpeditiousCastBuff = "ad7d2353-1959-45d8-84a2-7fb93f15f8a2";
    internal const string ExpeditiousCastResource = "cb3168e0-c05f-41b2-a9f3-c3fd7dfb34b3";

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

    internal const string GhostArmor = "d3b28fd9-e3db-415b-9754-012fa6aaccd8";
    internal const string GhostArmorAbility = "e1b880b2-b7b1-4c59-b571-8e838991ade2";
    internal const string GhostArmorBuff = "0ad47b7c-4790-4da6-821e-deec92371a61";
    internal const string GhostArmorShieldAbility = "80aee56b-d517-4e7d-8d45-acfc92376b6c";
    internal const string GhostArmorShieldBuff = "af372ef8-26cc-41b3-8301-6e653c98409f";

    internal const string Invulnerability = "03d89904-8e99-4856-8c9c-719e77463141";
    internal const string InvulnerabilityAbility = "bc659b72-52c3-4414-9c10-9d67e533d12d";
    internal const string InvulnerabilityBuff = "d77e2446-276a-4246-924e-d679b526eea8";

    internal const string Martyring = "78bf888f-1501-467a-9b0b-b17da3decebf";
    internal const string MartyringAbility = "ff1b7b02-aa03-42be-93c9-78d39d513c7d";
    internal const string MartyringBuff = "14df227c-794d-4328-acbb-389c9d9b081f";
    internal const string MartyringResource = "f6bc327f-4031-42b0-b068-16992ce73975";

    internal const string Rallying = "cfc8a849-c422-4043-b6e3-f96506b6b5b3";
    internal const string RallyingAbility = "2a403882-0466-4e6b-b4a0-7506a12f2427";
    internal const string RallyingBuff = "d513d85b-636f-46c0-9359-316bf7b23ba1";
    internal const string RallyingShieldAbility = "8d265df3-110b-43c1-b52e-fd7fa2947044";
    internal const string RallyingShieldBuff = "635a522c-880c-4eeb-a599-361ea3f6cb70";
    internal const string RallyingAura = "087eee7a-8632-4d9b-b5f3-074b23c52371";
    internal const string RallyingAuraBuff = "298dbc80-b704-446c-a3fd-efcdea5190ec";

    internal const string Reflecting = "88947511-2103-40b2-9c9e-4e26c2618952";
    internal const string ReflectingAbility = "58765ceb-079f-4a42-ae1e-83cb6957cdbd";
    internal const string ReflectingBuff = "93474eb1-e45e-40c5-a437-15963b72527e";
    internal const string ReflectingCastAbility = "5a5517af-27bb-4cc0-ac03-947e37472476";
    internal const string ReflectingCastBuff = "2c483050-4c83-4afc-b671-4ee96b29882f";
    internal const string ReflectingCastResource = "c6b43c97-649b-42e4-b026-37cae8bca7f3";

    internal const string Righteous = "851716fc-bb95-4b35-8fc5-690a590030fa";
    internal const string RighteousAbility = "bd4e66a7-4dd1-4747-96ba-423ccc32b32a";
    internal const string RighteousBuff = "9a5872b7-ca30-4f0b-98e2-8e2cb0fc6c4c";
    internal const string RighteousCastAbility = "aa581f86-1d74-4cc2-9e8b-d312719b88f1";
    internal const string RighteousCastResource = "ea9b5caf-e283-4614-b51f-8a60cd128b43";

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

    internal const string Wyrmsbreath = "834c540c-25f8-4341-956e-cf3d5c7b9172";
    internal const string WyrmsbreathAbility = "aa5a7b83-a942-4093-8589-ba293384b61e";
    internal const string WyrmsbreathBuff = "904e71ac-9dab-4880-92c0-63c3c33ebaab";
    internal const string WyrmsbreathResource = "a4fc0d20-70cf-4350-9c92-f7fe2ce987be";

    internal const string WyrmsbreathAcidAbility = "47d87415-0471-4a90-91ac-070dc3c74d7a";
    internal const string WyrmsbreathColdAbility = "7c9acbf9-3b16-4abd-b287-0697c342d303";
    internal const string WyrmsbreathElectricityAbility = "f6bdf2f2-97c8-4a3a-ad09-03c19984a121";
    internal const string WyrmsbreathFireAbility = "ee07cef2-167a-46ef-953b-e7a8e596b744";
    #endregion

    #region Legendary Weapon
    internal const string LegendaryWeapon = "d21246ce-21dc-4489-b3d4-746ec56ad4d4";
    internal const string LegendaryWeaponAbility = "f1c418f2-4184-41dd-ae6b-7b099c39fc55";
    internal const string LegendaryOffHandAbility = "d241dda4-6ae1-4d80-954c-d529e0ba8565";

    #region Bane
    internal const string Bane = "72f4ae63-19dc-48b2-9a0f-7deef17783cb";

    internal const string BaneParent = "e71f52a1-115a-4062-a369-2362a265ab14";
    internal const string BaneOffHandParent = "01a4d606-4fee-4f59-af5e-5e201ebd2533";

    internal const string BaneAberrations = "7bcd7911-eeb9-4453-b8d4-3dc21d99e37f";
    internal const string BaneAberrationsAbility = "26cd7f9c-3918-416d-bb18-76fd6c661473";
    internal const string BaneAberrationsBuff = "86bbf892-b3be-41f6-a4e0-3741dd9f80e6";
    internal const string BaneAberrationsOffHandAbility = "2ffcb62a-0f50-44fa-bea8-ba97e4bc44b0";
    internal const string BaneAberrationsOffHandBuff = "ce6e2ae7-cd8a-44a9-98b7-f57fbc196f78";

    internal const string BaneAnimals = "6e056395-b8f1-4c16-b2fa-607f8998138e";
    internal const string BaneAnimalsAbility = "20b09219-0b83-4237-9952-144c5716ca2a";
    internal const string BaneAnimalsBuff = "1392ee6e-2656-4ef4-9d70-e5acab76144b";
    internal const string BaneAnimalsOffHandAbility = "2680b317-724b-4622-9ee5-e16fedd5365e";
    internal const string BaneAnimalsOffHandBuff = "05dfac76-360b-47fc-8241-1c0a33da2221";

    internal const string BaneConstructs = "a0479565-72a2-47bb-a0de-d8497c3ed467";
    internal const string BaneConstructsAbility = "d578d3ad-a0fa-45dd-84e5-8b413887fe4a";
    internal const string BaneConstructsBuff = "0b3448b2-99c4-497a-b416-4e277d81fe81";
    internal const string BaneConstructsOffHandAbility = "87bf6bfb-a258-48b9-b4cf-eea26521015c";
    internal const string BaneConstructsOffHandBuff = "40e909df-2763-4b55-96cf-7aeee0f8debe";

    internal const string BaneDragons = "26dbc400-df84-41fe-b44a-d04224f5af59";
    internal const string BaneDragonsAbility = "4b9af299-1774-4028-b792-fdbf6af22933";
    internal const string BaneDragonsBuff = "c4137bad-890a-47dc-bfc9-2c31f974beeb";
    internal const string BaneDragonsOffHandAbility = "b62b6ffd-aca5-49ec-835f-69fed541cf7d";
    internal const string BaneDragonsOffHandBuff = "c7ff2350-8a07-4579-82ff-c10daf58e395";

    internal const string BaneFey = "90b5c7ec-433a-4ac2-b881-21a8aa4b6414";
    internal const string BaneFeyAbility = "a1222582-54c4-47ba-8ca9-17dc8267dd48";
    internal const string BaneFeyBuff = "7449b6e3-3e0e-485c-829d-744750356a90";
    internal const string BaneFeyOffHandAbility = "7a7fffe1-fcf7-441b-85e2-c44588687a93";
    internal const string BaneFeyOffHandBuff = "db82d7e1-fec9-4388-afeb-042bddebb12e";

    internal const string BaneHumanoidGiant = "93f16b42-8cbd-49a2-a276-e49d95f8a19e";
    internal const string BaneHumanoidGiantAbility = "6cd57c75-6406-46ae-a07e-271e134f9599";
    internal const string BaneHumanoidGiantBuff = "1343c94b-e317-4ddb-90d1-ddfaed3f9ec9";
    internal const string BaneHumanoidGiantOffHandAbility = "1ceb03c4-6d52-48d5-8125-2686ea1e68f8";
    internal const string BaneHumanoidGiantOffHandBuff = "4b35fe40-2cec-4926-8bd3-1893318946ef";

    internal const string BaneHumanoidReptilian = "9b1670b7-2850-46bb-bafc-38c8465187be";
    internal const string BaneHumanoidReptilianAbility = "c538d0ae-fe14-4675-98aa-7d514decbd27";
    internal const string BaneHumanoidReptilianBuff = "f3c52a04-2288-4981-a62a-40109c984f95";
    internal const string BaneHumanoidReptilianOffHandAbility = "b05e03af-b745-40e1-b201-011790072a47";
    internal const string BaneHumanoidReptilianOffHandBuff = "ffbbef60-cdd6-49df-854a-6ac2211d914d";

    internal const string BaneHumanoidMonstrous = "ef492a95-68e3-4c99-9d4c-9dfcddb41380";
    internal const string BaneHumanoidMonstrousAbility = "c5215f24-adf6-4680-9f52-6ab8a8cf53cc";
    internal const string BaneHumanoidMonstrousBuff = "44355137-0834-45d7-8d0c-933c2814dac5";
    internal const string BaneHumanoidMonstrousOffHandAbility = "7e8d4f98-27a3-4357-bfa6-948a7e6985d1";
    internal const string BaneHumanoidMonstrousOffHandBuff = "d95986d4-26fd-421c-bb6b-672d986b9d19";

    internal const string BaneMagicalBeasts = "373a41b5-7af0-4f31-b210-a8f415a1710a";
    internal const string BaneMagicalBeastsAbility = "6ab0aa75-d20d-4205-be2e-ea574ed64431";
    internal const string BaneMagicalBeastsBuff = "1f99a96c-c10a-46ca-93b5-ee821e3af99a";
    internal const string BaneMagicalBeastsOffHandAbility = "1984f80d-178b-43e7-b9ba-574cb3d14b8b";
    internal const string BaneMagicalBeastsOffHandBuff = "c9651ae6-b710-415a-b497-976896d588d0";

    internal const string BaneOutsiderGood = "7dd7c593-b2f5-4c9d-91b6-be8f79a2d591";
    internal const string BaneOutsiderGoodAbility = "bd73ee6f-6047-4ed7-97dc-77689fad2eea";
    internal const string BaneOutsiderGoodBuff = "c95e24ce-f033-4915-ba46-a29e632ea5a5";
    internal const string BaneOutsiderGoodOffHandAbility = "1b4cbc4a-52d3-4b19-b40c-77fa21a4d9b4";
    internal const string BaneOutsiderGoodOffHandBuff = "3ffbd7d7-c4ee-4624-b44d-0dcae088f59f";

    internal const string BaneOutsiderEvil = "20a979ee-1e38-4897-a08a-300ae46eedb3";
    internal const string BaneOutsiderEvilAbility = "633b8ffd-62e9-4378-9396-eef0eb7d6e6b";
    internal const string BaneOutsiderEvilBuff = "2b32f2da-0349-4d2b-886d-c6fb0b9efbc9";
    internal const string BaneOutsiderEvilOffHandAbility = "9dc39a25-c780-4db6-8f8d-9abf33ab3687";
    internal const string BaneOutsiderEvilOffHandBuff = "ab2b5426-5234-4a0d-a6d2-a76b29b79e3f";

    internal const string BaneOutsiderLawful = "e2bf5663-0abd-45dc-a0c7-b2806dca7d38";
    internal const string BaneOutsiderLawfulAbility = "e311444e-8816-4f5d-bbac-9ff159f0f50d";
    internal const string BaneOutsiderLawfulBuff = "c36b0a59-6242-4d07-8601-d637ae411187";
    internal const string BaneOutsiderLawfulOffHandAbility = "cd76446e-ca85-47fa-8f9c-49d71c53eac8";
    internal const string BaneOutsiderLawfulOffHandBuff = "69bdb177-ad92-43d2-a7c6-115b4a433331";

    internal const string BaneOutsiderChaotic = "b6733b68-4414-41c2-a06f-ee2d63e6a8d4";
    internal const string BaneOutsiderChaoticAbility = "b0f01aca-1db1-4b42-bd56-fe056e750bbb";
    internal const string BaneOutsiderChaoticBuff = "67a7f984-a0a5-47bb-a873-82cf4166c338";
    internal const string BaneOutsiderChaoticOffHandAbility = "ec887bc5-6d85-4cbc-9d2b-99f72eae777c";
    internal const string BaneOutsiderChaoticOffHandBuff = "8771bd44-970c-4d57-8db5-132609a00010";

    internal const string BaneOutsiderNeutral = "9c01329b-e9fc-4925-90d9-ef289a8f94e5";
    internal const string BaneOutsiderNeutralAbility = "654037f4-7bc0-4e39-8fa9-19dad814793c";
    internal const string BaneOutsiderNeutralBuff = "2fac2652-a5ec-44b7-a718-3dd979c4e2c3";
    internal const string BaneOutsiderNeutralOffHandAbility = "efb08a5e-bad5-43cc-a595-93da107c5d78";
    internal const string BaneOutsiderNeutralOffHandBuff = "1311b4ec-8691-4a18-95a3-875a206dceb4";

    internal const string BanePlants = "f747c114-bae0-4182-9b56-af361a2bf64a";
    internal const string BanePlantsAbility = "195ab6ed-0497-432c-91bd-2b55feca8e89";
    internal const string BanePlantsBuff = "89372337-a298-4cc3-aee0-3b2267ae031b";
    internal const string BanePlantsOffHandAbility = "90171cbe-debe-4086-9716-885bd251c711";
    internal const string BanePlantsOffHandBuff = "6bb662e7-fcc1-48f7-a382-64f8932b56c4";

    internal const string BaneUndead = "fd48c23b-9607-4b3d-bb11-0b117223637e";
    internal const string BaneUndeadAbility = "8d12b391-66e9-4efa-9d20-c2f2ab2881d2";
    internal const string BaneUndeadBuff = "816efdd2-7ed5-40c0-82a7-0f07160ef6f0";
    internal const string BaneUndeadOffHandAbility = "b9627943-d0a4-441e-a8b3-57103332109a";
    internal const string BaneUndeadOffHandBuff = "9eb8eb99-84b9-40fc-80e8-15a3e8cda69f";

    internal const string BaneVermin = "ecbd3f71-343d-4d77-8b69-6e2e982239cd";
    internal const string BaneVerminAbility = "f40857cf-24bc-4ccc-8f56-511d22276fc6";
    internal const string BaneVerminBuff = "9ac438a7-8a8b-4459-a0dd-a80b951a06a0";
    internal const string BaneVerminOffHandAbility = "2ceecea3-5fee-49a0-94c1-b757475bf9bf";
    internal const string BaneVerminOffHandBuff = "ff8f020d-485d-4eb4-9abd-00049c7e0fc8";
    #endregion
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