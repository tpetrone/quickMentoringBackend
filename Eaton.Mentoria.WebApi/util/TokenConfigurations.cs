namespace Eaton.Mentoria.WebApi.util
{
    public class TokenConfigurations
    {
        //configurações para a geração do token (Audience, Issuer e tempo de duração em segundos):
        
        //O destinatário pretendido do token. O aplicativo que recebe o token deve verificar se o valor de público-alvo está correto e rejeitar quaisquer tokens destinados a um público-alvo diferente.
        public string Audience { get; set; }

        //Identifica o STS (serviço de token de segurança) que constrói e retorna o token
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

}