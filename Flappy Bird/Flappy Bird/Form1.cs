using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class Form1 : Form
    {

        int pipeSpeed = 7; // bir tamsayı ile tanımlanan varsayılan boru hızı
        int gravity = 10; // bir tamsayı ile tanımlanan default yerçekimi hızı
        int score = 0; // default score integer set to 0

        public Form1()
        {
       
            InitializeComponent();
            gameTimer.Stop();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }



        // Borular arasındaki minimum ve maksimum mesafe (örnek değerler)
        const int minPipeGap = 100;
        const int maxPipeGap = 215;

        // Rastgele sayı üreteci
        Random random = new Random();



        private void gameTimerEvent(object sender, EventArgs e)
        {

           


            flappyBird.Top += gravity; // flappy bird resim kutusunu yerçekimine bağlayın, += yerçekimi hızını resim kutularının üst konumuna ekleyeceği anlamına gelir, böylece aşağı hareket eder
            pipeBottom.Left -= pipeSpeed; // alt boruların sol konumunu boru hızı tamsayısına bağlayın, boru hızı değerini boru resim kutusunun sol konumundan azaltacaktır, böylece her işaretlemede sola hareket edecektir
            pipeTop.Left -= pipeSpeed; //aynı şey üst boru için de geçerlidir, -= işaretini kullanarak borunun sol konumundan boru hızı tamsayısının değerini azaltın
            pipeTop2.Left -= pipeSpeed;
            pipeBottom2.Left -= pipeSpeed;


            scoreText.Text = "Score: " + score; // geçerli skoru skor metin etiketinde göster


                                         // Aşağıda herhangi bir borunun ekrandan çıkıp çıkmadığını kontrol ediyoruz
            if (pipeBottom.Left < -170)
            {
                // eğer alt boruların konumu -150 ise, o zaman 800'e geri sıfırlayacağız ve skora 1 ekleyeceğiz
               
                // Üst borunun rastgele bir yükseklik belirle
                int randomHeight = random.Next(minPipeGap, maxPipeGap);
                pipeTop.Height = randomHeight;
                pipeTop2.Height = randomHeight;

                // Alt borunun yüksekliğini hesapla (ekran yüksekliği - üst boru yüksekliği - sabit mesafe)
                int screenHeight = this.ClientSize.Height;
                pipeBottom.Height = screenHeight - pipeTop.Height - minPipeGap;
                pipeBottom2.Height = screenHeight - pipeTop2.Height - minPipeGap;

                pipeBottom.Left = 750;
                score++;
            }
            if (pipeTop.Left < -170)
            {
                                        // en üst boru konumu -180 ise boruyu 950'ye geri sıfırlarız ve skora 1 ekleriz
                pipeTop.Left = 750;
                score++;
            }
            if (pipeTop2.Left < -180)
            {
                                        // en üst boru konumu -180 ise boruyu 950'ye geri sıfırlarız ve skora 1 ekleriz
                pipeTop2.Left = 850;
                score++;
            }
            if (pipeBottom2.Left < -180)
            {
                                        // eğer alt boruların konumu -150 ise, o zaman 800'e geri sıfırlayacağız ve skora 1 ekleyeceğiz
                pipeBottom2.Left = 850;
                score++;
            }


                                         // aşağıdaki if deyimi borunun yere, borulara çarpıp çarpmadığını veya oyuncunun ekranı üstten terk edip etmediğini kontrol ediyor 
                                         // iki boru sembolü bir if deyiminin içinde VEYA anlamına gelir, böylece bu if deyiminin içinde birden fazla koşulumuz olabilir çünkü hepsi aynı şeyi yapacaktır
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
               flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
               flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25 || flappyBird.Bounds.IntersectsWith(pipeBottom2.Bounds)
               || flappyBird.Bounds.IntersectsWith(pipeTop2.Bounds)
               )
            {
                                          // yukarıdaki koşullardan herhangi biri karşılanırsa, oyun sonu işlevini çalıştıracağız
                endGame();
               
            }
            if (score > 5)
            {
                pipeSpeed = 12;           // puan 5'ten büyükse boru hızını 15'e çıkaracağız
            }

        }
                                           // bu, ana forma bağlı olan oyun tuşu aşağı olayıdır
        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Space)
            {
                if (!gameTimer.Enabled) // space tuşuna basıldığında oyun başlamamışsa başlat 
                {
                    // Oyun zamanlayıcısını başlat
                    gameTimer.Start();
                }                         // boşluk tuşuna basılırsa yerçekimi -14 olarak ayarlanır
                gravity = -10;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (isGameOver)                // Oyun bitmişse ve Space tuşuna basılmışsa
                {
                    // Oyunu başlatmak için gerekli işlemler

                    gameTimer.Start();
                    isGameOver = false;
                    score = 0;
                    scoreText.Text = "Score: " + score;
                                               // Kuş, borular ve diğer nesnelerin konumlarını sıfırla
                    flappyBird.Top = 212;
                    pipeBottom.Left = 537;
                    pipeBottom2.Left = 960;
                    pipeTop.Left = 537;
                    pipeTop2.Left = 960;
                    pipeSpeed = 7;

                    if (score > 5)
                    {
                        pipeSpeed = 15;           // puan 5'ten büyükse boru hızını 15'e çıkaracağız
                    }
                }
                else
                {
                                                // Oyun devam ediyorsa kuşu yukarı kaldır
                    gravity = -10;
                }
            }

        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            // bu, ana forma bağlı olan oyun anahtarı hazır olayıdır
            if (e.KeyCode == Keys.Space)
            {
                // boşluk tuşu bırakılırsa yerçekimi 14'e geri ayarlanır
                gravity = 10;
            }
        }
        private bool isGameOver = false;
        private void endGame()
        {
            // bu oyun sonu fonksiyonudur, bu fonksiyon kuş yere veya borulara dokunduğunda çalışacaktır
            gameTimer.Stop(); // ana zamanlayıcıyı durdur
            scoreText.Text += " Game over!!!";  // game over metnini skor metni üzerinde göster, += geçersiz kılmak yerine skorun yanına yeni metin dizesini eklemek için kullanılır
            isGameOver = true; // Oyun bittiğini işaretle

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
    }
}

