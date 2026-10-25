# Brake Toy — ortak agent çalışma rehberi

Güncelleme: 24 Ekim 2026. Codex ve Claude Code için ortak proje talimatıdır.
Öncelik: kullanıcının güncel talebi → bu rehber → konu belgesi → tarihsel kayıt.
Ortamın sistem/geliştirici talimatları ayrıca geçerlidir; belge bunları değiştirmez.

## 1. Önce doğru tabanı doğrula

- Unity/Git kökü bu dosyanın bulunduğu klasördür; üst çalışma alanı değildir.
- Son oyun teslimi v0.9.0 Adventure; sahne `Assets/BrakeToy/Scenes/ToyRoomAdventure.unity`.
- Oyun referans dalı `feat/adventure-v09`; bu belge paketinin dalı `docs/agent-handbook`.
- APK kaynak commit'i `b656017cb48c7b10b62891647135d0bc2371cfde`; kanıt `Reports/DELIVERY-v09.json`.
- Başlangıçta `git status --short`, geçerli dal ve son commit'i kontrol et.
- 24 Ekim tabanında `main` oyun kodu eski Faz 1'dir; README güncel sürüme yönlendirir.
- Sonraki merge'ler durumu değiştirebilir: dal adından güncellik varsayma, sahneyi doğrula.
- Çalışan ağacı sıfırlama; kullanıcı değişikliklerini koru. Doğru tabandan görev dalı kullan.
- Bu belge paketi oyun sürümünü, APK'yı veya test sonuçlarını yenilemez.
- 25 Ekim düzenleme dalı `chore/project-organization`: kanıtlar Reports/ altında,
  eski plan/kilometre taşları Docs/Archive/pre-v09/ altında; taşıma manifestini koru.

## 2. Ürün ve değişmezler

- Brake Toy / “NO BRAKES. BIG TROUBLE.”: fren yapamayan oyuncak araba, sonsuz arcade sürüş.
- Android öncelikli, portre, gerçek 3D; PC Unity Editor geliştirme/test ortamıdır.
- Çocuk odasındaki masayı ve odayı birlikte hissettiren renkli, okunabilir dünya hedeflenir.
- Ana hedef kitle çocuklar; yetişkinlere refleks/ustalık ve yüksek skor derinliği sunulur.
- Göreli yatay sürüklemeyle serbest X kontrolü vardır; şerit seçme düğmeleri yoktur.
- Yeşil yol erişilebilir kaçıştır; altın yol isteğe bağlı risk/ödüldür.
- Görsel model ve analitik çarpışma hacmi birlikte korunur; dekor tehlike gibi davranmamalı.
- Domino aktif Adventure/Tidy'den kaldırılmıştır; eski sahnede bulunması geri ekleme yetkisi değildir.
- Raf radyosu, kenar dekor boşlukları, kayıtlar ve müzik korunur; değişiklik açık göreve bağlanır.
- Ayrıntılı model/sanat kabulü açık; mevcut maketleri nihai kaliteli modeller diye sunma.
- Gerçek para/coin/IAP uygulanmadı; yeni sağlayıcı veya ücretli varlık kararını olmuş sayma.

## 3. Az ve doğru bağlam oku

- Her görev: bu dosya ve `YOL_HARITASI.md` içindeki ilgili iş paketi.
- Kod/entegrasyon: `MIMARI.md`; oynanış/denge: `MEKANIKLER.md`.
- Test/derleme/cihaz: `DOGRULAMA.md`; kapsam/karar: `KARARLAR.md`.
- Model işi: `MODEL_HANDOFF-v08.md` başındaki v0.9 eki ve ilgili prefab/test.
- `SOURCE_REQUIREMENTS.md` orijinal vizyondur; son kullanıcı kararlarını geçersiz kılmaz.
- Eski İngilizce tasarım/mimari/yol haritası ve PLAN/MILESTONE dosyaları tarihsel kayıttır.
- Belge başına en çok 300 satır; aşırı uzun satırlarla sınırı dolanma.
- Bütün raporları her turda yükleme; `rg` ile ilgili sınıf, anahtar ve senaryoyu bul.
- Kod ile belge çelişiyorsa farkı raporla; hedef davranışı sessizce uygulanmış sayma.

## 4. Teknoloji ve dosya haritası

- Unity `6000.3.26f1` / Unity 6.3 LTS, C#, URP `17.3.0`.
- Input System `1.17.0`, uGUI `2.0.0`, Test Framework `1.6.0`.
- Gerçek sürüm kaynağı `Packages/manifest.json`, `packages-lock.json`, ProjectVersion'dır.
- `Assets/BrakeToy/Scripts/Core/`: Unity bağımsız üretim, skor, ekonomi, çarpışma kuralları.
- `Assets/BrakeToy/Scripts/Runtime/`: yaşam döngüsü, giriş, havuz, görseller, kariyer, UI, istemci.
- `Assets/BrakeToy/Editor/`: sahne/model üretimi, render, doğrulama, Android derlemesi.
- `Assets/BrakeToy/Tests/EditMode/` ve `PlayMode/`: ayrı test assembly'leri.
- `Assets/BrakeToy/Scenes/`, `Art/`, `ScriptableObjects/`: varlık ve ayar sözleşmeleri.
- `Server/`: Python/SQLite `review-r1` referans servisi; üretim servisi değildir.
- Android: `com.braketoy.prototype`, ARM 64/IL 2CPP; son APK 0.9.0/kod 10.
- Son teslim test/debug sertifikalıdır; mağaza yayınına hazır imza sayılmaz.

## 5. Bir iş paketini tamamlama yöntemi

1. Güncel talebi, küçük iş paketini ve başarı ölçütünü belirle.
2. İlgili kodu/testi oku; varsayım, doğrulanmış davranış ve öneriyi ayır.
3. Yalnız bu paketi değiştir; ilgisiz refactor veya SDK ekleme.
4. İlgili kontrolleri çalıştır; hata nedenini düzelt, sonra gerekli regresyonu çalıştır.
5. Görsel/oynanış değişikliğinde gerçek motor veya cihaz kanıtı üret.
6. Günlük, etkilenen belge ve görev durumunu güncelle; commit/push/PR kanıtını bırak.
7. Sonucu, testleri ve kalan sınırları anlaşılır Türkçeyle bildir.

- Tüm oyunu tek turda bitirmeye çalışma; seçilen ve yetkilendirilmiş paketi tamamla.
- Yetkili işe gereksiz tekrar onayı isteme; eksik zorunlu bilgiyi somut gerekçeyle sor.
- Bağımlılıkları plana işle; ilgisiz sonraki kademeye sessizce geçme.
- Ayrı agent çalıştırmak kullanıcı/ortam yetkisine bağlıdır; bu belge görevi delege etme emri değildir.
- Agent'lar için dosya sahipliği belirle; aynı sahne/prefab/ayara eşzamanlı yazma.
- Sahne/prefab YAML çatışmasında rastgele taraf seçme; referansları editörde doğrula.
- Okunabilir kod yaz; yoğun tek satır stilini yeni koda yayma.
- Toplu mimari değişikliği ayrı, davranışı koruyan paketlere böl.

## 6. Oyuncu verisi ve uyumluluk

- `career-adventure-v1.json` güncel kariyerdir; eski `career-playground-v1.json` silinmez.
- İlk geçiş TP/araç/görev/güç stoklarını kopyalar, eski kurallı rekorları kopyalamaz.
- Save checksum bozulma kontrolüdür; anti-cheat çözümü değildir.
- Migration için örnek kayıt, yedek dönüşü ve yeniden açılış senaryosu gerekir.
- Rekorlar sürüme özgü kariyer ve ArcadeDirector anahtarlarından yüklenir; migration testlerini koru.
- Güçler ilk kariyere bir kez üçer hak verir; retry stok yenilemez.
- Testlerde kayıt/PlayerPrefs yazımını kapat; gerçek TP ve haklarla test yapma.
- Yardımlı tur kişisel ghost yazmaz; sıralama ayrımı ayrıca karar/doğrulama bekler.
- Adventure online'a kapalıdır; `ReviewOnline.Configured` Joyful bileşeninde false döner.
- Eski sunucuya yalnız endpoint ekleyerek yeni mekanik skorları gönderme.

## 7. Varlık ve doğrulama disiplini

- Eski çalışan sahne/prefabları koru; anlamlı değişiklikte varyant veya kontrollü migration kullan.
- Her küçük düzeltmede tüm sahne zincirini yeniden üretme.
- Unity `.meta` kimliklerini, ScriptableObject bağlarını ve hareketli pivotları koru.
- Builder'lar genel güncelleme aracı değildir; Create/upgrade davranışını önce incele.
- Mevcut sahneyi açmak için `Open` kullan; gereksiz `Create` çağırma.
- Sürüşte sürekli Instantiate/Destroy veya kare başına yeni koleksiyon ekleme.
- 25 Ekim K1.5 kanıtı 53 EditMode +71 PlayMode =124/124; APK aynı kaldı.
- Fiziksel v0.9 FPS/p95/ısı ve kullanıcı sanat/oynanış kabulü henüz tamamlanmadı.
- Test, cihaz ölçümü, kullanıcı onayı ve mağaza yayını ayrı kabul durumlarıdır.
- Belge değişikliğinde link/satır/çelişki kontrolü yeterlidir; gereksiz Unity/APK derleme.

## 8. GitHub, Notion ve devir

- Yetkili depo https://github.com/brscncn1/Brake-Toy ; biten paketi ilgili dala yükle.
- Force push, ilgisiz dosya üzerine yazma ve otomatik PR merge yapma.
- APK/AAB kaynak commit'ine girmez; GitHub Release varlığıdır.
- Kaynak SHA, test zamanı, sürüm ve paket SHA 256'sını birlikte kaydet.
- Anahtar/token, gerçek kayıt, Library/Temp/Logs ve yerel veritabanını commit etme.
- Notion merkezi https://app.notion.com/p/3ffdce66583181868ac1d4e4bd6feeac .
- Eşleştirme `Docs/NOTION_INDEX.json`; önemli karar/test/teslimi eşitle, geri okuyarak doğrula.
- Kullanıcı eklerini ve alt sayfaları koru; başarısız eşitlemeyi başarılı diye sunma.
- Önceki kota hatası tarihsel engeldir; kullanıcı güncelleme istediğinde erişimi yeniden kontrol et.
- Kota/erişim hatasında döngüyle tekrar etme; pending kaydı tut, yerel/GitHub işini bitir.
- Kullanıcı isteğiyle aktarılmayan notları masaüstündeki BrakeToy_Notion_Yedek.txt'ye de ekle.
- Bu yetki ücretli plan satın alma veya kimlik/güvenlik ayarı değiştirme yetkisi değildir.
- Devir: kapsam, dosyalar, kararlar, çalıştırılan kontrol, sonuç ve sıradaki tek paket.
