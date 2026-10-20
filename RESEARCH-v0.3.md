# v0.3 araştırma ve uygulama kararları

İnceleme: 21 Ekim 2026. Birincil geliştirici/yayıncı ve motor kaynakları esas alındı. Bu belge internetteki bütün yayınların eksiksiz tarandığı veya oyunların doğrudan oynanıp ölçüldüğü iddiası değildir. Erişilebilir metinler, bir GDC sunumu ve resmi görseller incelendi; Jetpack Joyride oturumunun yalnızca yayınlanan özeti okundu, videosu izlenmiş sayılmıyor. Rakiplerin bilinmeyen hız/üretim algoritmaları hakkında sayı uydurulmadı.

## Benzer oyunlardan çıkarımlar

- **Jetpack Joyride / Halfbrick:** [Luke Muscat, GDC 2012 — Depth in Simplicity](https://www.gdcvault.com/play/1015527/Depth-in-Simplicity-The-Making). Oturum özeti, kısa oturumlarla ödül yapısının birleşmesini anlatıyor. Bizim çıkarımımız: tek sürükleme kontrolünü korurken yakın geçişlerle risk/ödül ve hemen tekrar deneme eklemek. 10→18 hız eğrisi ve 2.5 saniye içinde ilk karşılaşma bizim deneme ayarımızdır; Halfbrick'ten alınan ölçümler değildir.
- **Hot Wheels Unleashed / Milestone:** [resmi çevre tanıtımı](https://1.hotwheelsunleashed.com/environments) ve [The Inside Track, chapter 2 — sanat/tasarım ekibi röportajı](https://hotwheelsunleashed.com/the-inside-track-chapter-2/). Ekip ortam seçimini mekaniklerle ilişkilendiriyor ve farklı yüzey/efektlerin performans bütçesini vurguluyor. Bizim çıkarımımız: masa dekoruyla oyun engelini ayırmak; odaya ait top, silgi ve kitapları aynı ölçek dilinde yapmak. Diecast oyuncak gövdesi, cam, metal, kauçuk ayrımıyla ölçek hissini güçlendirmek. Rakip marka/model/varlıkları kopyalanmadı.
- **Subway Surfers / SYBO:** [Celia Zimmermann, GDC 2023 sunumu](https://media.gdcvault.com/gdc2023/Slides/SubwaySurfers_Zimmermann_Celia.pdf), 22 sayfa; özellikle istekler, brief/demo/geri bildirim döngüsü. Bu kaynak engel hız algoritmasını açıklamıyor. Bizim çıkarımımız: kullanıcının 'sığ ve yavaş' geri bildirimini ölçülebilir ilk karşılaşma, hız, kaçış yolu ve görsel karşılaştırma hedeflerine çevirmek. [Unity'nin SYBO vaka yazısı](https://unity.com/resources/subway-surfers) prototip, sezgisel oynanış ve performans üzerinde birlikte çalışıldığını anlatıyor; oyunumuz için kanıtlanmış tutundurma oranı çıkarılamaz.

## Teknik kaynak ve kararlar

- [Unity — Mobile performance: graphics and assets](https://unity.com/blog/games/optimize-your-mobile-game-performance-expert-tips-on-graphics-and-assets): ortak materyaller, hareket etmeyen parçaları birleştirme, mipmap/texture bütçesi, reflection/postprocess maliyeti. Araç gövdesi ve her tekerlek materyal bazında birleştiriliyor; 24 tehlike başlangıçta oluşturulup tekrar kullanılıyor. Tek 1K ahşap diffuse/normal, küçük ortak reflection cubemap ve düşük örnekli/yarım çözünürlüklü SSAO kullanılıyor. FPS kazanımı henüz fiziksel cihazda ölçülmedi.
- Unity 6000.3.26f1 kurulu URP kaynak kodundaki `ScreenSpaceAmbientOcclusion.cs` ve [Unity SSAO açıklaması](https://docs.unity.com/en-us/engine/6000.7/manual/lighting-overview/lighting/shadows-in-urp/post-processing-ssao): yakın yüzeyler arasındaki temas gölgesi. Çevrimiçi belge daha yeni motor sürümüdür; uygulanacak alanlar kurulu paketten doğrulandı. Önceki renderer değiştirilmeden ayrı Workshop pipeline/renderer oluşturuldu.
- Sürekli çarpışma matematiği: top ile araç kutusunun XZ düzlemindeki Minkowski toplamı, iki dikdörtgen ve dört köşe dairesinin birleşimi olarak çözülüyor. İki eksende göreli hareket kullanıldığı için hızlı veya çapraz hareket kareler arasında atlanmıyor. Bu uygulama özgün koddur; kontrol edilen model sonlu yarıçaplı top ve eksen hizalı araç gameplay kutusudur, tam araç gövdesi fiziği değildir.

## Üretim ve adalet sınırı

Seed'li sıralama tek/duble kapı, yön değişimi, nefes satırı ve tek top olayı üretir. Her satır bir güvenli koridor bırakır; komşu koridorlar arasında geçiş sınırlıdır. Topun satırın yakınındaki yanal hareketi bu koridorun dışında tutulur; gelecekteki geometri başlangıçtan itibaren tanımlıdır. 95 birim ilerisi havuzlanır. Bu, tüm olası insan hareketlerinden kurtuluş garantisi vermez; bilerek kapalı tarafa geçmek yine çarpışmayla sonuçlanabilir. Keyfi engel birleşimleri üreten genel çözücü yerine bu sürüm doğrulanabilen sınırlı kalıp ailesini kullanır.

Hedef doğrulama: 128.000 satır için koridor/geçiş sınırı, 10.000 top çarpışma örneği, iki dakikalık motor içi tanık sürüşü, ilk karşılaşma, pause/retry, tek sefer Near Miss, dört portre oranı ve model sınır/mesh denetimi. Gerçek sonuçlar TEST_REPORT içinde yayımlanacak; burada yazılan test planı geçme kanıtı değildir.

## Varlık hakları

[Poly Haven Wood Table 001](https://polyhaven.com/a/wood_table_001), Dimitrios Savva (fotoğraf), Rico Cilliers (işleme). [Resmi lisans](https://polyhaven.com/license): CC0, yeniden dağıtım ve ticari kullanıma açık. Sadece indirilen 1K diffuse ve OpenGL normal dokuları kullanıldı; site metinleri/örnek renderları pakete eklenmedi. Kaynak metadata ve dosya özetleri `THIRD_PARTY_ASSETS.md` içinde.

Araç, tekerlekler, kitap, silgi, bloklar, top dikişleri ve yeni perde geometrisi proje içinde kodla oluşturulan özgün 3D modellerdir. Dışarıdan lisansı belirsiz model alınmadı. Bu sürüm fotogerçekçilik veya hatasızlık garantisi olarak sunulmaz; gerçek motor görüntüsü ve otomatik kontrollerle değerlendirilir.
