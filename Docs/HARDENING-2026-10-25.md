# Relax toparlanması ve Adventure kayıt ayrımı

Tarih: 25 Ekim 2026. Dal: `fix/adventure-relax-records`.
Taban: `cee3901b1db48471924aa68657c49feb6af6cb37` / PR #11.

## Doğrulanan sorunlar

1. Relax'ta yeni oyuncakla temas sonrası eski hazard havuzu temizleniyor,
   Adventure havuzundaki temas eden oyuncak kalıyordu. Yeni test aynı oyuncağın
   hâlâ aktif kaldığını doğruladı: beklenen false, gerçek true.
2. Adventure aynı `career-playground-v1.json` dosyasını kullanıyordu; oturumun stil
   yüklemesi de ArcadeDirector yerine sabit v0.8 anahtarına bakıyordu.
   İzole geçiş testi yeni anahtardaki 123 yerine eski 9000 mesafe rekorunu gördü.

## Uygulama

- Relax toparlanması yakın Adventure ve Playground proplarını, bağlı bariyerleri,
  rampaları ve zemin etkilerini temizler; uzaktaki karşılaşmalar korunur.
- Ayrı temaslar hâlâ ayrı hak tüketir: Relax üç hak, Arcade tek hak.
- Yeni kariyer `career-adventure-v1.json`; önceki dosya silinmez veya değiştirilmez.
- İlk geçişte önce Playground, yoksa Joyful, yoksa ilk kariyer kaynağı aranır.
- TP, araç sahipliği/seçimi, görev/başarım geçmişi, kozmetikler ve güç hakları korunur.
- Eski kurallı altı rekor alanı ve günlük rekor tarihi içeri alınmaz. Yeni v0.9
  PlayerPrefs mesafe/stil anahtarları okunur; kullanıcının bu anahtarlardaki rekoru korunur.
- Ortak eski dosyadaki v0.8/v0.9 sonuçlarının kökeni ayırt edilemez; bu dosya
  silinmeden tutulur, fakat karışık rekorları yeni profile topluca taşınmaz.
- Yeni profil veya yedeği varsa geçiş tekrarlanmaz; eksik ana dosyada kendi yedeği önceliklidir.
- Geçiş okuması tek başına dosya yazmaz; ilk normal başarılı kayıt yeni dosyayı oluşturur.
- Testler geçici dizin/benzersiz anahtar kullanır. SceneTestPersistence, sahne Start
  çalışmadan önce yazmayı kapatır; güç başlangıcı gerçek kariyere yazamaz.

## Kanıt ve sınırlar

- İlk iki hedefli test düzeltmeden önce 0/2, düzeltmeden sonra 2/2 geçti.
- Yeni kayıt testleri: ilerleme ve harcanan güçlerin korunması, tekrar yükleme,
  eski dosyanın değişmemesi, kendi yedeği, bozuk/eski kaynak ve yeni kariyer.
- Yeni oyun testleri: temas sonrası devam, uzak oyuncak/rampanın korunması,
  kaygan yüzey sıfırlaması, üç Relax/tek Arcade hakkı ve gerçek kayıt anahtarları.
- Nihai test sayısı ve XML hashleri `../Reports/HARDENING_VALIDATION.json` içinde kaydedilir.
- İlk geniş denemede test dosyalarındaki kodlama hatası üç testi bozdu; UTF-8
  kaynaklar düzeltilerek yeniden koşuldu. Yeni EditMode meta GUID hatası da düzeltildi;
  yeni testlerin gerçekten keşfedildiği nihai XML içinde kontrol edilir.
- APK derlenmedi; tablet performansı, görsel kabul ve mağaza yayını bu paket değildir.
- Sonraki paket: güç bitişi/iniş ve rampa-cetvel kenar uç durumları (K1.5).

## Notion ve masaüstü yedeği

- Yeni sayfa isteği bir kez denendi; entitlement_required / block_creation ile reddedildi.
- Yeni sayfa oluşturmak aynı çalışma alanının ücretsiz blok sınırını aşmaz.
- Otomatik tekrar veya ücretli plan işlemi yapılmadı.
- Kullanıcı isteğiyle masaüstünde `BrakeToy_Notion_Yedek.txt` tutulur; bekleyen
  önceki kayıtlar ve bu paketin doğrulanmış sonuçları sonradan aktarım için eklenir.
- Dosya GitHub'daki pending/günlük kayıtlarına ek yedektir; Notion'a aktarılmış sayılmaz.
