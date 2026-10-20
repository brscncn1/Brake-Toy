# v0.3 kalite değerlendirmesi ve düzeltme önerileri

Bu rapor geliştiricinin kendi çıktısına yönelik değerlendirmesidir. Kaynak: v0.3 gerçek Unity oynanış, 24 saniyelik Fever sürüşü, ön/arka model renderları, kaynak kodu ve test kapsamı. Yeni kullanıcı denemesi veya fiziksel telefon ölçümü yapılmış gibi sunulmaz.

## Genel sonuç

v0.3 teknik olarak daha dolu bir prototiptir; tutarlı ve cilalı bir oyun bölümü seviyesine ulaşmadı. Onaylanan konseptle aradaki fark hâlâ büyüktür. Önceki sunumlarda varlık sayısı ve geçen test sayısı, algılanan kaliteye gereğinden fazla vekil oldu. Teknik başarı, eğlence veya sanat kalitesi kabulü değildir.

## Beğenmediğim sonuçlar

1. **Görsel bütünlük — kritik.** Ahşap, araç ve oda farklı ayrıntı düzeyleri taşıyor; pencere/poster/mobilyalar basit geometriler olarak kalıyor. Fotoğraf dokusu tek başına gerçekçilik sağlamadı. Öneri: tek oda için malzeme, ölçek, ışık ve kompozisyonu birlikte ele alan tutarlı bir kalite çalışması; yeni harita/garajı bundan sonra büyütmek.
2. **Masa ve gölge hiyerarşisi — yüksek.** Koyu ahşap deseni ve uzun pencere gölgeleri oyuncaklardan fazla dikkat çekiyor. Öneri: daha sakin yüzey kontrastı, daha yumuşak gölge, belirgin araç/engel silueti. Gerçek doku korunabilir ama baskınlığı ayarlanmalı.
3. **Araç oranları — yüksek.** Jant/lastik/ayna detaylarına rağmen kabin kutu gibi ve camlar büyük koyu yüzeyler. Öneri: kabini alçaltmak, gövde/cam oranını düzeltmek, camı okunabilir oyuncak malzemesi yapmak. Vitrin görüntüsü yerine oyun kamerasında da kontrol; gameplay kutusunu korumak.
4. **Kamera ve UI — yüksek.** Üst gösterge şeridi odayı örtüyor; araç küçük kalıyor. Menüde v0.2'den kalan Menu card ile yeni panel üst üste. Öneri: daha düşük/uygun kamera ve yan boşluk ayarı, küçük köşe göstergeleri, tek panel, sürüş sırasında gereksiz düğmeleri gizleme. Maksimum hızda 1.5 saniyelik tepki görüşü korunmalı.
5. **Karşılaşma tekrarı — yüksek.** Aynı seed ve sınırlı kapı ailesi kısa sürede ezberlenebilir. Öneri: tanımlı engel kataloğu, çeşitlenen satır aralıkları/sıraları, art arda aynı güvenli koridoru azaltma; yeni tur ile aynı parkuru tekrar etme seçeneklerini ayırma. Geçilebilirlik testleri yeni kalıplar için de çalışmalı.
6. **Top davranışı — orta/yüksek.** Zamanlama ve yanal yollar fazla düzenli; hareketli tehlike hissi sınırlı. Öneri: güvenli koridor dışında farklı kesişim noktaları/eğimler, değişken olay aralığı; taraf uyarısını ve görünür tepki süresini koruma.
7. **Ses/çarpışma cilası — orta.** Kodda motor tek sentez döngüsüne, diğer tepkiler kısa temel tonlara dayanıyor. Profesyonel ses tasarımı seviyesinde değil; bu değerlendirme yeni bir dinleme testi iddiası değildir. Öneri: kontrollü katmanlar, daha anlamlı oyuncak çarpışması ve daha iyi ses/görsel zamanlaması; aşırı efekt yerine okunabilir tepki.
8. **Performans kanıtı — kritik açık.** Cihaz FPS/ısı/dokunma ölçülmedi. Renderer/üçgen sayımı gerçek telefon profili değildir. Öneri: düşük/yüksek grafik seçenekleri, gölge/SSAO/render ölçeği bütçeleri; ardından gerçek cihaz matrisi. Editörün hızlı olması telefon kabulü sayılmamalı.
9. **Sunum ve kapsam — süreç sorunu.** Konsept hedefiyle mevcut çıktı ve planlananla tamamlanan iş daha açık ayrılmalı. Öneri: aynı koşullarda gerçek motor önce/sonra görüntüleri, açık kabul kriterleri ve ölçülmeyen alanların belirtilmesi. Faz sayısı kaliteyi kanıtlamaz; tam kapsamda yayın hazırlığı Faz 6'dır.

## v0.4 için öncelikli uygulama

Kullanıcının kapsam kararıyla dokuz başlığın tümü v0.4 çalışmasına alındı: görsel bütünlük, yüzey/ışık, araç, kamera/UI, karşılaşma/top çeşitliliği, ses/çarpışma, grafik profili/ölçüm ve gerçek önce/sonra kanıtı. Uygulama ile fiziksel cihaz/insan sanat ve eğlence kabulü ayrı kaydedilir; garaj/ekonomi/yeni harita/online eklenmez.

## Kabul koşulları

- Eski v0.3 sahne ve varlıkları korunur; ayrı v0.4 sahnesi/config/varlıkları.
- Menüde tek panel, daha küçük HUD; dört portre oranında temel kontroller/tepki alanı görünür.
- Araç orantıları ve yüzey kontrastı aynı koşullardaki gerçek Unity renderlarıyla karşılaştırılır; geometri/kutu sınırı kontrol edilir.
- Tanımlı katalog ile havuz boyutu/tehlike ölçüsü eşleşir; farklı turlar üretilebilir, aynı seed yeniden üretilebilir. Yeni kalıpların güvenli tanık rotaları sınanır.
- Grafik profilleri seçilebilir ve önceki sahnelerin ayarlarını kirletmez. Gerçek cihaz performansı hâlâ ayrı kabul maddesidir.
- Test sonuçları, APK, GitHub ve Notion kayıtları gerçek sonuçlarla güncellenir.

## Kullanıcının kapsam kararı — tüm öneriler bu sürümde

Kalite raporundaki dokuz başlık aynı v0.4 çalışmasına alındı. Ses ve çarpışma cilası sonraya bırakılmayacak: katmanlı motor/teker sesi, kuru oyuncak çarpışması ve kısa görsel tepki. Oda ayrıntıları ve gerçek motor önce/sonra karşılaştırması da teslimde yer alacak. Fiziksel cihaz FPS/ısı/dokunma kabulü için cihaz matrisi ve ölçüm kaydı hazırlanacak; cihaz olmadan ölçüldü denmeyecek. Önceki dar kapsam ifadeleri bu kararla güncellendi. Yeni harita/garaj/online bu kalite raporunun önerileri arasında değildir.
