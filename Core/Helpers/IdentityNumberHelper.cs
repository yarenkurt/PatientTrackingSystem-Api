using Core.Extensions;

namespace Core.Helpers
{
    public static class IdentityNumberHelper
    {
        public static bool Verify(string identityNumber)
        {
            if (identityNumber.Length < 11)
                return false;

            if (identityNumber.Substring(0, 1) == "0")
                return false;

            var n1 =
                identityNumber[0].ToString().ToInt() +
                identityNumber[2].ToString().ToInt() +
                identityNumber[4].ToString().ToInt() +
                identityNumber[6].ToString().ToInt() +
                identityNumber[8].ToString().ToInt();

            var n2 =
                identityNumber[1].ToString().ToInt() +
                identityNumber[3].ToString().ToInt() +
                identityNumber[5].ToString().ToInt() +
                identityNumber[7].ToString().ToInt();

            var result = (n1 * 7 - n2) % 10;

            if (result != identityNumber[9].ToString().ToInt())
                return false;

            var n3 = 0;
            for (var i = 0; i < 10; i++)
                n3 += identityNumber[i].ToString().ToInt();

            return n3 % 10 == identityNumber[10].ToString().ToInt();
        }
    }
}