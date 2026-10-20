# BRAKE TOY

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerçek 3D URP / Android öncelikli portre.

## Güncel sürüm: v0.2 — Bedroom Tabletop

Sparky artık çocuk odasındaki ahşap oyun masasının üzerinde ilerliyor. Çevrede pencere/perde, oyuncak rafı ve ayı, yatak, poster, sandalye, oda zemini ve masa ayakları var. Masa üzerindeki defter/kalemlik ve çıkartmalar, zeminle aynı ilerlemeyi kullanır; oda çevresi sabit referans sağlar. Bu, onaylanan konsept yönündeki **oynanabilir bir ara sürümdür**; nihai sanat kalitesi değildir.

![Gerçek Unity v0.2 Play Mode görüntüsü](Preview-v0.2.png)

- [GitHub kaynakları](https://github.com/brscncn1/Brake-Toy)
- [v0.2 Android prototip](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.2.0)
- [v0.1 ilk prototip](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.1.0)
- [Notion proje merkezi](https://app.notion.com/p/3ffdce66583181868ac1d4e4bd6feeac)

## Aç ve oyna

1. Unity Hub → Projects → Add project from disk ile bu **BrakeToy** klasörünü seç. Assets klasörünü tek başına seçme.
2. Unity **6000.3.26f1** veya aynı 6.3 LTS serisinin uygun yamasını kullan. İlk açılışta Unity paketlerinin indirilmesini bekle.
3. **Tools → Brake Toy → Open Bedroom Tabletop v0.2** ile sahneyi aç. Doğrudan dosya: `Assets/BrakeToy/Scenes/ToyRoomTabletop.unity`.
4. **Game** penceresinin oran listesinden **9:16** seç. Yoksa **+ → Fixed Resolution → 540 × 960** ekle. Editörde Free Aspect geniş bir görüntü verebilir; telefon hedefi portredir.
5. Üstte **Play**, oyunda **OYNA**. Sol fare tuşunu tutup sağa/sola sürükle. Telefonda tek parmak kullan.
6. İlk dokunuş arabayı ışınlamaz; bırakınca yatay konum korunur. Çarpışmada dünya durur, **TEKRAR OYNA** baştan başlatır. **II** duraklatır; pencere odağı kaybolunca da sürüş durur.

Eski `ToyRoom.unity` ve Faz 1 varlıkları karşılaştırma için korundu. Yeni sürüm ayrı sahne, konfigürasyon ve prefablar kullanır. Elle referans bağlaman gerekmez.

## Android APK

Yerel dosya: `Builds/Android/BrakeToy-v0.2-Tabletop.apk` — yaklaşık **37.0 MiB**, ARM64 / IL2CPP / Development, sürüm 0.2.0 (kod 2). Minimum API 25, hedef API 36. İmza, arşiv bütünlüğü ve paket bilgileri doğrulandı. Ayrıntı/SHA-256: `ANDROID_BUILD_REPORT.json`.

Android 7.1+ ARM64 cihazda APK ile dene veya USB geliştirici bağlantısıyla **File → Build Profiles → Android → Build And Run** kullan. Paket kimliği `com.braketoy.prototype` olduğu için yeni APK, ilk prototipin güncellemesi olarak yüklenir. Fiziksel cihazda henüz denenmedi; APK üretimi FPS/dokunma onayı değildir.

Yeniden derleme: **Tools → Brake Toy → Build Android Prototype**. Hub modülleri: Android Build Support + SDK & NDK Tools + OpenJDK. **Player → Other Settings → Active Input Handling: Input System Package (New)** olmalı. APK bir mağaza yayını değildir.

## Ayarlar ve görsel üretim

- Aktif konfigürasyon: `Assets/BrakeToy/ScriptableObjects/RoomConfig.asset`.
- Masa 12 birim, sürüş alanı 7 birim genişliğinde. Sparky gameplay collider'ı önceki sürümle aynı: 0.78 × 0.70 × 1.40.
- Kamera 20° / dikey FOV 50°; araba alt %25'te. 28° karşılaştırması: `Preview-v0.2-28deg.png`. Oda daha çok göründüğü için 20° seçildi.
- Kontrol hassasiyeti 9, maksimum yatay hız 9.5, dünya hızı 8. Bu ara sürüm hızlanma veya kombo eklemez.
- Modeller/materialler: `Assets/BrakeToy/Art/Tabletop`; prefablar: `Assets/BrakeToy/Prefabs/Tabletop`.
- Kaynak sahne üreticisi: `TabletopSceneBuilder`. **Create Bedroom Tabletop v0.2** sahne yoksa üretir; mevcut sahnenin üzerine yazmaz. **Refine Tabletop Surfaces** yalnızca bu ara sürümün üretilmiş varlıklarını güncelleyen geliştirme aracıdır; kişisel sanat düzenlemelerinden sonra çalıştırma.

## Test durumu

**Unity EditMode 19/19 ve PlayMode 8/8: toplam 27/27 geçti.**

Kontrol/collision çekirdeği, orijinal sahnenin giriş/döngüsü, yeni oda sahnesinde collider/dekor ayrımı, 4 ekran oranında kamera/tepki görüşü, retry ve yakın dekorların kayma/reset davranışı kontrol edildi. Kaynaklar: `Tests/EditMode-v0.2-results.xml`, `Tests/PlayMode-v0.2-results.xml`, `UNITY_TEST_RESULTS.json`.

Unity Test Runner: **Window → General → Test Runner → EditMode/PlayMode → Run All**. Testler sırasında oyuna elle dokunma. Bağımsız çekirdek çalıştırıcısı (.NET 8 SDK):

```powershell
dotnet run --project Tests/Standalone/BrakeToy.CoreChecks.csproj -- TEST_RESULTS.json
```

`TEST_REPORT.md` kapsam ve sınırları açıklar. Gerçek telefon dokunmatiği, safe area/kesintiler, uzun seans FPS/bellek/ısı ve iOS doğrulanmadı. 60 FPS hedefi ölçülmüş başarı olarak sunulmaz.

## Belgeler ve çalışma düzeni

- `SOURCE_REQUIREMENTS.md`: başlangıç promptunun eksiksiz, değiştirilmemiş kaynağı.
- `GAME_DESIGN.md`, `ARCHITECTURE.md`, `ROADMAP.md`: nihai kapsam ve aşamalı mimari/plan.
- `VISUAL_DIRECTION.md`, `Concepts/`: onaylanan konsept, üretim istemi ve güncel uygulama notları.
- `ROOM_MILESTONE.md`: v0.2 teslim ve sınırları. `TESLIM_RAPORU.md`: önceki Faz 1 teslimi.
- `DEV_LOG.md`, `CHANGELOG.md`, `AGENTS.md`: geliştirme günlüğü ve sonraki çalışmaların kuralları.
- `Docs/NOTION_INDEX.json`: belgelerin ayrı Notion sayfaları. Önemli karar, uygulama, test ve teslimlerde Notion güncellenir; bağlantı kesilirse yerel günlük korunur. Notion ücretsiz blok ek süresi 24 Ekim 2026 00.41 Türkiye saatinde sona erebilir.
- `Docs/Archive/v0.1`: önceki sürümün doğrulama raporları. Library/Temp/native ara dosyaları Git'e alınmaz; APK'lar GitHub Releases'te tutulur.

Reklam/analiz/ödeme veya çevrimiçi oyun SDK'sı eklenmedi. Near Miss, kombo, prosedürel adalet, garaj, Relax ve online sistemler sonraki fazlardadır. Önce yeni oda sürümünün gerçek sürüş hissi kontrol edilecek.
