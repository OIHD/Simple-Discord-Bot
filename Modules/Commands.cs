using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Linq;
using Discord.WebSocket;

namespace SIMPLEBOT.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        bool RoleCheck(string Role)
        {
            if (Context.User is SocketGuildUser user)
            {
                if (user.Roles.Any(r => r.Name == Role))
                {
                    return true;
                }

                else
                {
                    Console.WriteLine("Yetkisiz eylem");
                    return false;
                }

            }

            else
            {
                Console.WriteLine("Yetkisiz eylem");
                return false;
            }
        }

        [Command("test")]
        public async Task test(string Mesaj)
        {
            if (RoleCheck("Developer"))
                await ReplyAsync("Bu komudu kullanmak için yeterli yetkin var");
            else
                await ReplyAsync("Bu komudu kullanmak için yeterli yetkin yok");
        }

        [Command("social")]
        public async Task social(string MSoc)
        {
            if (RoleCheck("Developer"))
            {
                ITextChannel social = Context.Client.GetChannel(928734856861605949) as ITextChannel;
                await social.SendFileAsync(MSoc);
            }
        }

        [Command("update")]
        public async Task update(int numara, string format)
        {
            if (RoleCheck("Developer"))
            {
                ITextChannel DuyurMKanal = Context.Client.GetChannel(926455186556342292) as ITextChannel;
                await DuyurMKanal.SendFileAsync($"C:/SIMPLEBOT/{numara}.{format}", "");
            }
        }

        [Command("intro")]
        public async Task intro(string numara, string format)
        {
            if (RoleCheck("Developer"))
            {
                ITextChannel DuyurMKanal = Context.Client.GetChannel(931569553404751972) as ITextChannel;
                await DuyurMKanal.SendFileAsync($"C:/SIMPLEBOT/{numara}.{format}", "");
            }
        }

        [Command("Kurallar")]
        public async Task Kurallar(string Kdil, string RMesaj)
        {
            if (RoleCheck("Developer"))
            {
                var EmbedBuilder = new EmbedBuilder()
                        .WithThumbnailUrl("http://resimurl")
                        .WithTitle("simple BOT - " +
                                  (
                        (Kdil == "TR") ? (":flag_tr: Kurallar") :
                        (Kdil == "EN") ? (":flag_gb: Rules") : (" ")
                        )
                        )
                        .WithDescription(RMesaj)
                        .WithFooter(footer =>
                        {
                            footer
                            .WithText("simple BOT")
                            .WithIconUrl("http://resimurl");
                        });
                Embed embed = EmbedBuilder.Build();
                ITextChannel DuyurMKanal = Context.Client.GetChannel(926569422016090172) as ITextChannel;
                await DuyurMKanal.SendMessageAsync(embed: embed);
            }
        }

        [Command("Duyur")]
        public async Task Duyur(string DMesaj)
        {
            if (RoleCheck("Developer"))
            {
                await ReplyAsync("Duyuru Komudu Gönderildi");
                ITextChannel DuyurMKanal = Context.Client.GetChannel(926463181650546718) as ITextChannel;
                await DuyurMKanal.SendMessageAsync("`" + DateTime.Now + " | BOT Announcement : `\n```" + DMesaj + "```");
            }
        }




        [Command("SeseGec")]
        public async Task Ses(SocketVoiceChannel voiceChannel)
        {
            if (RoleCheck("Developer"))
            {
                if (voiceChannel == null) return;

                var connection = await voiceChannel.ConnectAsync();

            }
        }


        [Command("Devlog")]
        public async Task DevLog(int surumNo, string DLMesaj)
        {
            if (RoleCheck("Developer"))
            {

                string surumNoVer = "Mesaj Girilmedi";
                surumNoVer = surumNo.ToString();
                if ((surumNoVer.Length > 2) || (surumNoVer.Length == 0))
                {
                    await ReplyAsync("DevLog Mesaj Komutu Gönderildi");
                    ITextChannel DuyurMKanal = Context.Client.GetChannel(926455042037399642) as ITextChannel;

                    surumNoVer = " " + surumNoVer[0] + "." + surumNoVer[1] + "." + surumNoVer[2];
                    await DuyurMKanal.SendMessageAsync("**BOT | ** Version" + surumNoVer + "\n```" + DLMesaj + "```");
                }
                else
                {
                    await ReplyAsync("Komut Gönderlilemedi !\n" +
                        "3 Haneli bir sürüm numarası giriniz ve çift tırnak içinde mesajınızı yazınız.");
                }
            }
        }


        

        [Command("Sorumetni")]
        public async Task Sorumetni()
        {
            await ReplyAsync("Hello world.");
        }

        [Command("Komutlar")]
        public async Task Komutlar(int sayfano)
        {
            switch (sayfano)
            {
                case 1:
                    await ReplyAsync("test //geliştirici test komutudur\n" +
                        "Duyur //Duyuru kanalına bir duyuru gönderir\n" +
                        "Sorumetni //Hello world \n" +
                        "Komutlar //Tüm komutları size gösterir\n" +
                        "\n" +
                        "Sonraki sayfaya geçmek için Komutlar " + (sayfano + 1) + " komutunu kullanın");
                    break;
                case 2:
                    await ReplyAsync("Daha fazla komut bulunmamaktadır\n" +
                        "Devlog //Devlog kanalına bir duyuru atar\n"+
                        "SeseGec //Ses kanalına geçer\n" +
                        "update //Galeri kanalına dosya yükler\n"+
                        "intro //Intro kanalına dosya yükler\n"+
                        "\n" + 
                        "Sonraki sayfaya geçmek için Komutlar " + (sayfano + 1) + " komutunu kullanın");
                    break;
                default:
                    break;
            }

        }

    }
}


