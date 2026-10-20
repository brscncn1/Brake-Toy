# v0.3 doğrulama raporu

Unity 6000.3.26f1: **41/41 geçti** — 26 EditMode, 15 PlayMode. XML sonuçları Tests klasöründe. Önceki 19 çekirdek ve 8 PlayMode regresyonu korunuyor.

## Gerçekleştirilen kontroller

- Hızın hemen yükselmesi, başlangıç 10 / tavan 18, 10 saniyede >13, 30 saniyede >16; ilk karşılaşma 1.5–2.5 saniyede. 30/60/120 FPS hız integrali karşılaştırması.
- 1000 seed × 128 satır = 128.000 satırda güvenli koridor, yan geçiş zamanı, tekrar azaltma ve hareketli topun koridora taşmaması. Bu sınırlı kalıp ailesinin tanık rota kontrolüdür; tüm insan hareketlerinin veya keyfi engel düzenlerinin garantisi değildir.
- 10.000 göreli hareketli top-kutu çarpışma örneği, ince zaman örnekli oracle ve temas noktasının geometrik doğrulaması. AABB köşesi yerine daire-kutu çözümü.
- İki dakikalık motor içi tanık sürüşünde 60'tan fazla satır; çarpışma ve havuz tükenmesi yok, tehlike örnek sayısı sabit. Bu gerçek insan oynanış testi değildir.
- Yakın geçişin yalnızca tam geçişten sonra ve bir kez ödüllendirilmesi; kombo/zaman aşımı/reset; hareketli topta sürekli çarpışma; crash/retry.
- Duraklamada hız/mesafe/kombo zamanının ilerlememesi, deterministik retry ve aynı 24 tehlike örneğinin tekrar kullanılması.
- Combo Fever görsel şeritleri: 8 yakın geçişte aktif; hız ve collider değişmiyor, çarpışmada kapanıyor.
- Araç modelinin gameplay kutusuna sığması, lastiklerin zeminin altında olmaması, 4 tekerlek bağlantısı; top dönerken gerçek vertexlerinin zemine girmemesi. Mesh vertex/indis geçerliliği, dekor collider'larının olmaması.
- 9:16, 9:19.5, 9:21, 3:4 oranlarında maksimum hızdaki 1.5 saniyelik tepki mesafesi görünür.
- Gerçek Unity oynanış ve ön/arka/engel yakın plan renderları incelendi. Farın gövde içine kesilmesi, lastiğin kaputtan çıkması, kaporta normalleri ve top dönme merkezi düzeltildi. Bu kontroller hatasızlık garantisi değildir.
- Android ARM64 IL2CPP Development APK: başarılı build, ZIP bütünlüğü, native kitaplık, v2 imzası, sürüm/paket/API kontrolleri.

## Açık doğrulamalar

Fiziksel Android dokunma/FPS/ısı/safe area/kesintiler ve uzun kullanım; farklı GPU'larda SSAO/materyal görünümü; gerçek oyuncuda tempo ve risk ödülü; iOS. İnsan oyuncunun her seçiminin kurtarılabilir olduğu iddia edilmez. Kaynak görseller hedef kaliteyi tam olarak karşılayan final art olarak sunulmaz.

SSAO yarım çözünürlük/4 örnek, 4x MSAA, 2048 gölge haritası, iki cascade ve gölgesiz dolgu ışığının mobil maliyeti gerçek cihazda ölçülecek. Düşük kalite profili henüz eklenmedi.
