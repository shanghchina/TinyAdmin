using AutoMapper;
using System.Collections;
using System.Collections.Generic;

namespace Tiny.OPS.Contract
{
    //public void testme()
    //{
    //    UserDto dto = new UserDto
    //    {
    //        Name = "Niko",
    //        PassWord = "1234",
    //    };
    //    User user = dto.MapTo<User>();
    //    user.Name.ShouldBe("Niko");
    //    user.PassWord.ShouldBe("1234");
    //    user.Id.ToString().ShouldBe("0");
    //}

    /// <summary>
    /// AutoMapper扩展类
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 对象到对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static T MapTo<T>(this object obj)
        {
            return Mapper.Map<T>(obj);
        }

        /// <summary>
        /// 集合到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static List<T> MapTo<T>(this IEnumerable obj)
        {
            return Mapper.Map<List<T>>(obj);
        }
    }
}
