private void webScrapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var webGet = new HtmlWeb();
            ScrapingBrowser bbr = new ScrapingBrowser();
            string url = "SourceURL###";

            var document = webGet.Load(url);
            WebClient webClient = new WebClient();
            try
            {
                var imagesOnPage = from imgs in document.DocumentNode.Descendants()
                                      where imgs.Name == "img" &&
                                           imgs.Attributes["src"] != null
                                      select new
                                      {
                                          Src = imgs.Attributes["src"].Value,
                                      };

                comboBox1.Items.Clear();
                try
                {
                    int i = 0;
                    foreach (var img in imagesOnPage)
                    {
                        
                        comboBox1.Items.Add(img.Src);
                        string imgurl = img.Src;
                        if (imgurl[0] == '/')
                        {
                            imgurl = "SourceURL###" + imgurl;
                        }
                        var image = webGet.Load(imgurl);
                        var path = @"destinationDirectory###";                        
                        var fileName = i;
                        webClient.DownloadFile(imgurl, path + fileName.ToString() + ".png");
                        i++;
                       
                    }
                    MessageBox.Show("Images Downloaded Successfully!!");
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }