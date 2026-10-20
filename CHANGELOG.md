# v0.3.0 — Arcade Workshop

- Erken/sürekli hızlanma; seed'li tek/duble geçitler, yön değişimi ve nefes satırları.
- 24 örnek havuzu, uyarılı çapraz toplar ve sürekli göreli daire-kutu çarpışması.
- Tek sefer Near Miss, kombo/timeout/Fever, ayrı yerel mesafe/stil rekorları, sonuç ekranı.
- HUD/vites/yön uyarısı, özgün sentez sesleri ve ses kapatma.
- Özgün diecast araç, yeni kitap/silgi/blok/top modelleri, gerçek CC0 ahşap, pencere açıklığı, perde/abajur, ayrı renderer/SSAO/reflection ve oda mesh birleştirmesi.
- 41 Unity testi, 128.000 satır / 10.000 top çarpışması kontrolü ve Android APK.

# Değişiklik kaydı

## 0.2.0 — Bedroom Tabletop, 21 Ekim 2026

- Ayrı çocuk odası/masa sahnesi, oda mobilyaları ve kırtasiye dekorları.
- Yuvarlatılmış oyuncak mesh'leri, plastik/ahşap materyalleri ve tekerlek animasyonu.
- 20° oda kamerası; 28° karşılaştırması ve daha hafif menü örtüsü.
- World ilerlemesi/reset ile eşzamanlı yakın dekor akışı; kontrol/collider değerleri korundu.
- Unity 27/27 test geçti; Android ARM64 IL2CPP Development APK üretildi.
- GitHub deposu/ilk APK Release ve özel Notion proje merkezi/ayrı belgeler/günlük kuruldu.
- Gerçek cihaz FPS/dokunma testi ve nihai art tamamlanmadı.


## 0.1.0 — Faz 1 kaynak hazırlığı, 21 Ekim 2026

- Ana gereksinim belgesi eksiksiz korundu; tasarım, mimari ve yedi fazlık yol haritası yazıldı.
- Unity 6.3 LTS, URP 17.3, Input System 1.17, uGUI ve Test Framework manifesti eklendi.
- Dünya ölçeği, aspect'e göre perspektif kamera ve portre ayarları tanımlandı.
- Göreli dokunma/mouse, yatay sınır/hız, bırakma/yeniden dokunma ve görsel yatış eklendi.
- Sabit dizilimli tekrar kullanılan 7 zemin/8 engel; sürekli XZ çarpışma ve hızlı retry kaynakları eklendi.
- Ready/Running/Paused/Crashed ve odak kaybı yönetimi; Türkçe temel UI/safe area eklendi.
- Primitive Sparky, üç engel tipi, zemin, ortak materyal, mobil URP ve bağlantılı sahne üreten korumalı editör aracı yazıldı.
- 19 ortak C# kontrolü geçti; 10.000 seed çarpışma karşılaştırması dahil. Unity EditMode 19/19 ve PlayMode 4/4 geçti; gerçek Input System mouse/touch/UI testleri dahil.
- Unity kuruldu; sahne/prefab/URP assetleri motor içinde üretildi ve Play Mode 3D önizlemesi incelendi.
- Mouse basılı durum geçişinin izlenmesi, sanal giriş testinde bulunan başlangıç olayı sorunu üzerine düzeltildi.
- Unity/Android kurulumu ve doğrulama adımları belgelendi. Fiziksel cihaz ve manuel sürüş hissi kabulü açık bırakıldı.

- Android ARM64 IL2CPP Development APK başarıyla derlendi; imza/arşiv ve paket bilgisi kontrol edildi. Fiziksel cihaz testi yapılmadı.
