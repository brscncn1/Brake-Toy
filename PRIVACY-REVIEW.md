# Gizlilik ve mağaza hazırlığı — taslak

Bu belge mevcut revizyon adayının veri davranışını açıklar. Yayınlanmış gizlilik politikası veya hukuki uygunluk onayı değildir.

## Mevcut çevrimdışı paket

- Hesap zorunlu değildir. Reklam, satın alma, analytics veya sohbet SDK'sı yoktur.
- Kariyer, puanlar, seçilen araç/kozmetik ve ghost yalnızca uygulamanın yerel klasöründe tutulur.
- İsteğe bağlı performans ölçümü cihaz modeli/işletim sistemi/FPS/p95 bilgilerini yalnızca yerel dosyaya yazar.
- Online endpoint boş ve uygunluk seçeneği kapalıdır. Oyun otomatik oturum açmaz ve skor yollamaz.
- Günlük antrenmanda kullanılan UTC tarih cihazdan gelir; sunucu zamanı diye sunulmaz.
- Yerel veriler uygulama kaldırıldığında kaybolabilir. Son manifestte allowBackup özel olarak kapatılmamış; işletim sistemi/OEM yedekleme mekanizması yerel verileri kullanıcı ayarlarına göre yedekleyebilir. Bu, uygulamanın kendi bulut senkronizasyonu değildir; üretim politikası ve yedekleme tercihi ayrıca kesinleştirilmelidir.

## İleride etkinleştirilecek servisin farklı davranışı

Referans servis açıkça seçilen online turda rastgele oturum kimliği, seed, süre/yatay girdi ve sunucuda türetilen skor işler. Sunucuda skor ve geçici tur bileti tutulur. Ağ işletimi sırasında IP adresi görülür; referans kodu bunu yalnızca kısa süreli oran sınırlaması için bellekte kullanır. Üretim barındırıcısının günlük/backup/retention davranışı henüz seçilmedi. Üretim politikası bu taslaktan doğrudan kopyalanamaz.

## Yayın öncesi sahibinin tamamlayacağı bilgiler

- Yayıncı/yasal sorumlu adı ve iletişim adresi, politika URL'si.
- Gerçek hedef yaş kitlesi ve dağıtım bölgeleri; Families/Kids kategorisi ve gerekli izin akışları.
- Nihai veri toplama/SDK envanteri; Google Play Data safety ve ilgili mağaza beyanları.
- Online seçilirse sağlayıcı, saklama/silme/erişim/hesap kurtarma yöntemi ve gerekli inceleme.
- Kullanıcıya ait kalıcı imza anahtarı, Play Console kaydı, içerik derecelendirmesi ve gerçek cihaz kapalı testleri.

Çocukların kullanımı hedeflendiği için online ve reklam uygunluk kapısı kodda kapalı tutulur. Ebeveyn kapısı ile hukuki izin aynı şey değildir. Reklam/IAP bu adayda uygulanmadı; uygunluk/gelir modeli kararı olmadan etkinleştirilmez. Kullanıcı zorluk, müzik ve görsel kaliteyi ayrıca revize edecek; bunlar mağaza kalite kabulü bekleyen maddelerdir.

## İncelenen birincil kaynaklar

23 Ekim 2026 hazırlık incelemesi; yayın tarihinde yeniden kontrol edilir. Bu kaynakların okunması uygulamayı uygun ilan etmez.

- [Google Play Families](https://support.google.com/googleplay/android-developer/answer/9893335?hl=en): hedef kitle, veri beyanı ve çocuklara yönelik reklam koşulları.
- [Apple Kids tasarım kaynağı](https://developer.apple.com/kids/) ve [App Review Guidelines](https://developer.apple.com/app-store/review/guidelines/#kids-category): iOS dağıtımı seçilirse ayrıca değerlendirilecek kurallar. Bu Windows çalışmasında iOS derlemesi yapılmadı.
- [FTC güncel COPPA kuralı](https://www.ftc.gov/legal-library/browse/rules/childrens-online-privacy-protection-rule-coppa): FAQ, 2025 değişikliği için doğrudan güncel kurala yönlendiriyor; eski FAQ tek başına kabul kanıtı sayılmadı.
- [AB Komisyonu çocuk verileri açıklaması](https://commission.europa.eu/law/law-topic/data-protection/information-business-and-organisations/legal-grounds-processing-data/are-there-any-specific-safeguards-data-about-children_en): rızaya dayalı işlemlerde ülkeye göre değişen eşikler ve anlaşılır bilgilendirme.
- [Android 16 KB sayfa boyutu kılavuzu](https://developer.android.com/guide/practices/page-sizes): APK ZIP ve native ELF hizalama kontrolünün dayanağı. Hizalama kontrolü gerçek 16 KB cihaz testinin yerine geçmez.
