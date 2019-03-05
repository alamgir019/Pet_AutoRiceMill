using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AutoRiceMill.Localization
{
    public static class AutoRiceMillLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AutoRiceMillConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AutoRiceMillLocalizationConfigurer).GetAssembly(),
                        "AutoRiceMill.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
