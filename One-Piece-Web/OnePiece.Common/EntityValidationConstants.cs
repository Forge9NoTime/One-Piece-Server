namespace OnePiece.Common
{
    public static class EntityValidationConstants
    {
        public static class Volunteer
        {
            public const int FullNameMinLenght = 10;
            public const int FullNameMaxLenght = 100;

            public const int MinPhoneNumberLenght = 10;
            public const int MaxPhoneNumberLenght = 13;

            public const int MinAge = 0;
            public const int MaxAge = 150;
        }
        
        public static class Organizer
        {
            public const int MinPhoneNumberLenght = 10;
            public const int MaxPhoneNumberLenght = 13;

            public const int ExactEGNLenght = 10;

            public const int PlaceOfResidencyMinLenght = 5;
            public const int PlaceOfResidencyMaxLenght = 100;

            public const int AffiliatedOrganizationMinLenght = 5;
            public const int AffiliatedOrganizationMaxLenght = 100;

            public const int MinOrganizerPhoneNumberLenght = 10;
            public const int MaxOrganizerPhoneNumberLenght = 13;
        }

        public static class Team
        {
            public const int NameMinLenght = 1;
            public const int NameMaxLenght = 30;

        }
        public static class TeamType
        {
            public const int NameMinLenght = 2;
            public const int NameMaxLenght = 100;
        }

        public static class Transport
        {
            public const int MinAvailableSpots = 0;
            public const int MaxAvailableSpots = 30;
        }

        public static class Mission
        {
            public const int TitleMinLenght = 5;
            public const int TitleMaxLenght = 100;

            public const int LocationMinLenght = 5;
            public const int LocationMaxLenght = 100;

            public const int DescriptionMinLenght = 10;
            public const int DescriptionMaxLenght = 1000;
        }

        public static class ThreatLevel
        {
            public const int NameMaxLenght = 100;
        }

        public static class MissionType
        {
            public const int NameMinLenght = 2;
            public const int NameMaxLenght = 100;
        }

        public static class Order
        {
            public const int MinQuantity = 0;
            public const int MaxQuantity = 1000;
        }

        public static class Item
        {
            public const int MinQuantity = 0;
            public const int MaxQuantity = 1000;

            public const int ItemMinLenght = 1;
            public const int ItemMaxLenght = 50;
        }

        public static class ItemType
        {
            public const int NameMinLenght = 2;
            public const int NameMaxLenght = 100;
        }
    }
}
