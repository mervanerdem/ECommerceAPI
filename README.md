# E-CommerceAPI

## Bitirme Projesi İçerik
Proje kapsamında sadece dijital ürünler satan bir platform geliştirilmesi beklenmektedir. Hedefimiz Mobil ve web uygulaması olmak üzere 3 farklı kanal üzerinden satış yapan bir platform için uygulama geliştirmektir. (Android,IOS,Web). 
Proje kapsamında dijital ürün veya ürün lisansları satışı yapılmaktadır. Kullanıcılar dijital ürün veya ürün lisansları satan sisteme kayıt yaptırarak alışveriş yapabilirler. 
Sadakat sistemi ile çalışan bu sistemde kullanıcılar alışveriş yaptıkça extra puan kazanmaktadır. 
Kullanıcılar kazandıkları puanları bir sonraki alışverişte kullanarak yeni ürünleri indirimli bir şekilde alabilmektedirler. Bununla birlike kupon sistemi sayesinde hediye kuponlar ile sepet tutarı üzerinden daha uygun fiyatlı alışveriş yapabilmektedir. 
Proje 4 ana başlık altında toplanmıştır. Senaryoda yoruma açık olan kısımlar yorumlanarak kodlanabilir. 
 Kullanıcı işlemleri
Sistemde iki farklı kullanıcı rolü vardır. Bunlar: normal kullanıcı ve admin kullanıcılardır. Kayıt sayfasından kayıt ol apisi ile sisteme yeni bir kullanıcı kayıt olur ve kullanıcı adı şifre bilgisini kendisi belirler. Sonrasında hesabına login olup alışverişe başlayabilir. Hesap üzerinde dijital cüzdan olup ödeme işlemleri anlık olarak gerçekleştirilmelidir. Ödeme sırasında kart bilgileri alınarak sepet tutarı kadar ücret kullanıcıya yansıtılır. 
Kart bilgileri sistemde saklanmamalıdır. Kullanıcı almak istediği tüm ürünleri tek bir request de sipariş apisine ileterek işlemi gerçekleştirir. 
Kredi kartı ile ödeme için harici bir servis geliştirilebilir. (isteğe bağlı.) Admin role sahip kullanıcılar sistem sahibi olarak tüm işlemler için yetkilidir. Sistem üzerinde dijital ürün tanımı yapıp silebilir. Güncelleme ve fiyat belirleme işlemi yapabilir. Kupon tanımı yapabilir. Kullanılan ve kullanılmayan tüm kuponları görebilir. Kategori işlemlerini yapabilirler. 
Admin kullanıcılar için kayıt işlemi ayrı bir api üzerimden sadece admin kullanıcı tarafından sağlanmalıdır.  Kullanıcı ekleme silme güncelleme işlemleri yapılabilir. Role değişikliği yapılamaz.
 ### Ürün işlemleri
Admin yetkisine sahip kullanıcı tarafından ürün işlemleri gerçekleştirilir. Yeni ürün ekleme silme güncelleme işlemleri yapılır. Fiyat bilgisi belirlenir. Fiyatlar net fiyat olup herhangi harici bir hesaplama yapılmasına gerek yoktur. Ürün durumunu belirten bir alan ile filtreleme yapılabilmelidir. Bazı ürünler satışta olmayabilir. Bazı ürünler için yetersiz stok olabilir. Ürün üzerinde stok bilgisine müdahale etmeyi sağlayacak ilgili apiler de hazırlanmalıdır. 
Ürün üzerinde kategori bilgisi olmalıdır. Her ürün birden fazla kategoriye ait olabilir. M-M ilişki kurulmalıdır. Kategori üzerinde url, tag gibi alanlar olmalıdır. Ürünler kategori bazlı listelenebilmelidir. 
Ürün tasarımı değiştirip farklı eklemeler yapabilirsiniz. 
Kategori tanım ve güncelleme işlemleri, ürünlerin kategori seçimleri admin kullanıcı tarafından yapılmalıdır. 
Ürün üzerinde her satışta kazanılabilecek puan miktarını belirleyen bir alan vardır ve bu alan üründen satışta %x tutarında size anında puan kazandırır. Bu alan yüzdelik olup max bir limit ile sınırlandırılmıştır. Örneğin, 100 TRY tutarında bir ürün aldığınızda puan kazanma oranı %12 ve MaxPuan değeri 10 TRY ise 12 TRY kazanmanız gerekirken üst limit olan 10 TRY değerini aştığınız için 10 TRY olarak extra puan kazanacaksınız. 
Kazanılan puanlar cüzdan hesabına aktarılacak olup bir sonraki alışveriş sırasında kullanılabilecektir. 
Kullanıcı sonraki alışverişi sırasında 80 TRYʼ lik bir ürün satın alırken öncelikle 10 TRY önceki alışverişten kazandığı puan düşülecek. Sonrasında kalan 70 TRYʼlik tutar için 70*0.12 = 8.4 TRY puan kazanacaktır. 
Harcanılan puan tutarı için puan kazanılmayacaktır. Sadece net ödenen tutar üzerinden puan kazanımı olabilir. 
### Kupon
Admin kullanıcılar belli aralıklar ile kuponlar oluşturup kullanıcılara bunları iletir ve alışverişlerinde kullanmalarını ister. Kuponlar belli bir tutarda oluşturacak olup unique bir kod ile ayırt edilir. Kod sistemde sadece bir kere oluşmalı. Max 10 karakter uzunluğunda olmalıdır.
Kullanıcılar kendilerine iletilen bu kuponları ödeme adımında kupon kodunu girerek sepet tutarından düşmesini beklemektedir. Ödeme adımında girilen kupon kod geçerli ve aktif bir kod ise kupon tutarı kadar sepet tutarında indirim yapılır. 
Kupon kullanımı sırasında kupondaki bakiye üzerinden puan kazanımı olmaz. Kupon kullanımı ve kullanıcının cüzdan bakiyesi yeterli ise işlem ücretsiz gerçekleşir. 
Kupon ve puanın birlikte kullanımı durumunda önce kupon fiyattan düşülür. Sonrasında eksik kalan miktar puanlardan tamamlanır ya da tamamı kullanılır. Puan ve kupon tutarı yeterli olmadığı kısımda kredi kartından tahsilat yapılır. Ödeme sırasında sadece bir kupon kullanılabilir. Kullanılan kupon tekrar kullanılmaz. 
Kuponların geçerlilik tarihi olmalı ve sadece o süre içerisinde kullanılabilir olmalı. Kuponlar ürün bazlı değil sistem genelinde kullanılabilecek şekilde olmalı. Kullanılan kupon tek başına sepet tutarı için yeterli ise işlem puan kullanmadan ücretsiz gerçekleşir ve puan kazanımı olmaz. 
### Raporlama
Sepet içerisinde birden fazla ürün olabilir. Tek bir siparişte birden fazla aynı ya da farklı ürün siparişi verilebilir. 
Her sipariş için bir sipariş numarası oluşturulmalı ve max 9 karakter uzunluğunda numerik olmalı. 
Sipariş tablosu üzerinde sepet tutarı, kullanılan kupon kodu, kupon tutarı, kullanılan puan miktarı alanları olmalı. 
Sipariş detay tablosu üzerinde sepetteki ürünler ve onların tutar vs. bilgileri yer almalı. 
### Db modelleri 
Kullanıcı (ad, soyad, email, role, şifre, statü, dijital cüzdan bilgileri, puan bakiyesi ,vs) 
Kategori (adi. url, tags vs,) 
Ürün (Kategori, adi, özellikleri, açıklama, aktiflik, puan kazandırma yüzdesi, max puan tutarı) 
Ürün Kategori Map (kategoriId,UrunId) Many-To-Many ilişkili yapı kurulması Sipariş (sepet tutarı, kupon tutarı, kupon kodu, puan tutarı, vs.) 
Sipariş Detay(sepetteki urun detay bilgileri vs.,)
### İhtiyaçlar 
Kategori ekleme güncelleme apileri. Listeleme apileri. 
Kategori bazlı urun listeleme apisi . 
Kategori silme apisi (Kategoride ürün varsa silinemez) 
Ürün ekleme güncelleme apisi. Listeleme apisi. Silme apisi 
Kullanıcı oluşturma apisi. Login apisi. Kullanıcı güncelleme ve silme apisi. Yetkilendirme icin jwt token altyapısı 
Sipariş apileri, oluşturma , aktif siparişler, geçmiş sipariş apileri 
Sipariş detay apisi, siparişteki ürün bilgileri 
Kupon oluşturma listeleme ve silme apileri 
Puan sorgulama apisi 
Tech-Stack 
Veri tabanı (Postgresql,Mssql) 
JWT token (Yetkilendirme) 
EF-Repositroy - Unitofwork 
Postman veya Swagger 
Rabbitmq (Opsiyonel) 
Redis (Opsiyonel) 
Teslim kriterleri 
Proje kapsamında sadece swagger üzerinde bu senaryonun uçtan uça çalışması beklenmektedir. 
Postman yada herhangi bir api dökümantasyon aracı kullanarak sistem dökümante edilmeli. 
CodeFirst yada DbFirst yaklaşımlarından birisi ile ilerleyebilirsiniz. Code fist geliştirme yaptıysanız yeni bir db olustuğunda migrationların çalıştığından emin olunuz. 
Db first geliştirme yaptıysanız proje tesliminde db backup ve scriptleri ekleyiniz. Projenizin başarılı şekilde derlendiğinden emin olunuz. 
### Kriterler 
Değişken isimleri anlamlı, amaç neyse ona göre verilmiş. 
Metot isimleri ile metodun amacını net ifade edilmiş. 
Class'ların içindeki metot sayısı az ve amaca yönelik belirlenmiş.
İç içe if ler olmayacak. Complexity düşük tutulmuş. 
Ayni kod parçasının tekrarlandığı duruma yer verilmemiş. 
Kodu okumayı zorlaştıran conditional complexity yaratılmamış Uzun metotlar (25 satırdan uzun olmamali. 
Hardcoded değerler Const olarak isimlendirerek kullanılmış. 
Aynı kodlama standardı tüm kodlarda uygulanmış. Farklı dosyalar arasında tutarsızlık yaratılmamış. 
Class'ların içindeki metotlar tek bir sorumluluk alanında odaklı yazılmış. Objectler arası bağımlılıklar enjekte edilmiş. 
Dependency Injection kullanılmış. 
Classlar arası bağımlılıkların en azda tutulmasına dikkat edilmiş. İnterface gibi abstraction lar ihtiyaç olduğu için kullanılmış. Gereksiz abstraction eklenmemiş. 
İnterface içeriği az ve tek sorumluluğa özgü metot imzası barındıracak şekilde tasarlanmış. Gereksiz design pattern kullanımı yapılmamış. 
Gerçekten bir problem çözmek için kullanılmış. 
Open-Closed Prensibine dikkat edilmiş. Kod genişlemeye açık, değişikliğe kapalı şeklinde tasarlanmış. Web/REST standartlarına dikkat edilmiş. Rest Api de açık metot parametreler için defansif validation kodları yazılmış.


## Postman Döküman
https://documenter.getpostman.com/view/20212429/2s93z6ejTq

## Ek Notlar
ilk giriş için kullanılacak admin girişi
```JSON
{
  "email": "admin@dartvader.com",
  "password": "password"
}
```
