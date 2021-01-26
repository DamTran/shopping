namespace Taste.Utility.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringHelpers
    {
        private static readonly Regex CamelCaseRegex;

        static StringHelpers()
        {
            const string pattern = @"
                (?<!^) # Not start
                (
                    # Digit, not preceded by another digit
                    (?<!\d)\d
                    |
                    # Upper-case letter, followed by lower-case letter if
                    # preceded by another upper-case letter, e.g. 'G' in HTMLGuide
                    (?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z])
                )";
            const RegexOptions options = RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled;
            CamelCaseRegex = new Regex(pattern, options);
        }

        /// <summary>
        ///     <para>Split a camelCase string and separate each word with specific seperator.</para>
        ///     <para>Examples:</para>
        ///     <para>  "PurchaseOrders"  => "Purchase Orders"    </para>
        ///     <para>  "purchaseOrders"  => "purchase Orders"    </para>
        ///     <para>  "2Unlimited"      => "2 Unlimited"        </para>
        ///     <para>  "The2Unlimited"   => "The 2 Unlimited"    </para>
        ///     <para>  "Unlimited2"      => "Unlimited 2"        </para>
        ///     <para>  "222Unlimited"    => "222 Unlimited"      </para>
        ///     <para>  "The222Unlimited" => "The 222 Unlimited"  </para>
        ///     <para>  "Unlimited222"    => "Unlimited 222"      </para>
        ///     <para>  "ATeam"           => "A Team"             </para>
        ///     <para>  "TheATeam"        => "The A Team"         </para>
        ///     <para>  "TeamA"           => "Team A"             </para>
        ///     <para>  "HTMLGuide"       => "HTML Guide"         </para>
        ///     <para>  "TheHTMLGuide"    => "The HTML Guide"     </para>
        ///     <para>  "TheGuideToHTML"  => "The Guide To HTML"  </para>
        ///     <para>  "HTMLGuide5"      => "HTML Guide 5"       </para>
        ///     <para>  "TheHTML5Guide"   => "The HTML 5 Guide"   </para>
        ///     <para>  "TheGuideToHTML5" => "The Guide To HTML 5"</para>
        ///     <para>  "TheUKAllStars"   => "The UK All Stars"   </para>
        ///     <para>  "AllStarsUK"      => "All Stars UK"       </para>
        ///     <para>  "UKAllStars"      => "UK All Stars"       </para>
        /// </summary>
        public static string SplitCamelCase(string input, string separator = " ")
        {
            return CamelCaseRegex.Replace(input, separator + "$1");
        }

        /// <summary>
        /// Check if a <see cref="delimitor"/> separated string contains a set of <see cref="keywords"/>.
        /// </summary>
        /// <example>
        /// To check if "1,2,3" contains "1" or not, please do:
        /// <see cref="CheckKeywordsInDelimitedText"/>("1,2,3", ',', "1")
        /// </example>
        /// <param name="text"></param>
        /// <param name="delimitor"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static List<object> CheckKeywordsInDelimitedText(string text, char delimitor, params object[] keywords)
        {
            if (string.IsNullOrEmpty(text)) return new List<object>();

            text = $"{delimitor}{text.Trim(delimitor)}{delimitor}";
            return keywords.Where(keyword => text.Contains($"{delimitor}{keyword}{delimitor}")).ToList();
        }
    }
}