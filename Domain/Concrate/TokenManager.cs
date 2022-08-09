
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

//namespace Domain.Concrate
//{

//    public static string GenerateToken(string userName)
//    {

//        byte[] key = Convert.FromBase64String(Secret);
//        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
//        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[] {
//                      new Claim(ClaimTypes.Name, userName)}),
//            Expires = DateTime.UtcNow.AddMinutes(30),
//            SigningCredentials = new SigningCredentials(securityKey,
//            SecurityAlgorithms.HmacSha256Signature)
//        };

//        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
//        JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
//        return handler.WriteToken(token);
//    }
}
