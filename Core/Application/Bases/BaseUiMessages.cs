using System.Globalization;

namespace Application.Bases
{
    public class BaseUiMessages
    {
        public static string NOT_FIND_DATA = "Məlumat tapılmadı";

        public const string MINIMUM_100_SYMBOL_MESSAGE = "100 simvoldan az daxil edilə bilməz";
        public const string MINIMUM_3_SYMBOL_MESSAGE = "3 simvoldan az daxil edilə bilməz";
        public const string MINIMUM_11_SYMBOL_MESSAGE = "11 simvoldan az daxil edilə bilməz";
        public const string MAXIMUM_3000_SYMBOL_MESSAGE = "3000 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_2000_SYMBOL_MESSAGE = "2000 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_1000_SYMBOL_MESSAGE = "1000 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_500_SYMBOL_MESSAGE = "500 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_200_SYMBOL_MESSAGE = "200 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_300_SYMBOL_MESSAGE = "300 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_50_SYMBOL_MESSAGE = "50 simvoldan çox daxil edilə bilməz";
        public const string MAXIMUM_20_SYMBOL_MESSAGE = "20 simvoldan çox daxil edilə bilməz";
        public const string NOT_EMPTY_MESSAGE = "Boş data daxil edilə bilməz";
        public const string PICKUPDATE_GREAT_NOW = "Başlama tarixi indiki zamandan böyük olmalıdır";
        public const string DROPOFFDATE_GREAT_PICKUPDATE = "Bitiş tarixi başlama tarixindən böyük olmalıdır";
        public const string GREATER_THAN_0 = "0-dan böyük olmalıdır";
        public const string GREATER_THAN_1 = "Birdən böyük olmalıdır";
        public const string YEAR_LESSTHAN_NOW = "İndiki tarixdən kiçik olmalıdır";
        public const string MAXIMUM_100_SYMBOL_MESSAGE = "100 simvoldan çox daxil edilə bilməz";
        public const string NOT_VALID_EMAIL = "E-poçt doğru deyil";
        public const string NOT_VALID_PHONENUMBER = "Telefon nömrəsi doğru deyil";

        public const string PASSWORD_LESS_THAN_LETTER = "Şifrəniz 8 elementdən azdır";
        public const string PASSWORD_GREAT_THAN_LETTER = "Şifrəniz 16 elementdən çoxdur";
        public const string PASSWORD_NOT_CONTAIN_UPPERCASE = "Şifrəniz kiçik hərflər ehtiva etmir";
        public const string PASSWORD_NOT_CONTAIN_LOWERCASE = "Şifrəniz böyük hərflər ehtiva etmir";
        public const string PASSWORD_NOT_CONTAIN_NUMBER = "Şifrəniz rəqəm ehtiva etmir";
        public const string PASSWORD_NOT_CONTAIN_SYMBOL = "Şifrəniz ən az birini ehtiva etməlidir (!.@.#)";

        public const string EXCEPTION_TITLE_NOT_SAME = "Başlıq eyni ola bilməz";
        public const string EXCEPTION_EMAIL_OR_PASSWORD_NOT_VALID  = "Email və ya şifrə yanlışdır";
        public const string EXCEPTION_USER_ALREADY_EXISTS  = "Bu istifadəçi artıq qeydiyyatdan keçib";

        public static string SuccessAddedMessage(string propName)
        {
            return $"{propName} uğurla əlavə olundu";
        }
        public static string SuccessUpdatedMessage(string propName)
        {
            return $"{propName} uğurla yeniləndi";
        }
        public static string SuccessDeletedMessage(string propName)
        {
            return $"{propName} uğurla sistemdən silindi";
        }
        public static string SuccessCopyTrashMessage(string propName)
        {
            return $"{propName} zibil qutusuna köçürüldü";
        }

        public static string SuccessReturnTrashMessage(string propName)
        {
            return $"{propName} zibil qutusundan çıxarıldı";
        }

    }
}
