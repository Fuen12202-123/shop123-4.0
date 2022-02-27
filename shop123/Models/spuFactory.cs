using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class spuFactory
    {
        shop123Entities2 db = new shop123Entities2();

        public List<spu> queryAll()
        {
            var spu=db.spu.ToList();
            return spu;
        } 
        public List<spu> queryShowDesc()
        {
            var spu=db.spu.Where(m => m.spuShow == "已上架").OrderByDescending(m => m.spuEditTime).ToList();
            return spu;
        }
        public List<spu> queryBycatA(int catalogAId)
        {
            var spu = db.spu.Where(m => m.catalogAId == catalogAId && m.spuShow == "已上架").OrderByDescending(m => m.spuEditTime).ToList();
            return spu;
        }
        public List<spu> queryBycatAB(int catalogAId, int catalogBId)
        {
            var spu = db.spu.Where(m => m.catalogAId == catalogAId && m.catalogBId == catalogBId && m.spuShow == "已上架").OrderByDescending(m => m.spuEditTime).ToList();
            return spu;
        }
        public List<spu> queryBycatAasc(int catalogAId)
        {
            var spu = db.spu.Where(m => m.catalogAId == catalogAId && m.spuShow == "已上架").OrderBy(m => m.spuPrice).ToList();
            return spu;
        }
        public List<spu> queryBycatABasc(int catalogAId, int catalogBId)
        {
            var spu = db.spu.Where(m => m.catalogAId == catalogAId && m.catalogBId == catalogBId && m.spuShow == "已上架").OrderBy(m => m.spuPrice).ToList();
            return spu;
        }
        public List<spu> queryBycatAdesc(int catalogAId)
        {
            var spu = db.spu.Where(m => m.catalogAId == catalogAId && m.spuShow == "已上架").OrderByDescending(m => m.spuPrice).ToList();
            return spu;
        }
        public List<spu> queryBycatABdesc(int catalogAId, int catalogBId)
        {
            var spu = db.spu.Where(m => m.catalogAId == catalogAId && m.catalogBId == catalogBId && m.spuShow == "已上架").OrderByDescending(m => m.spuPrice).ToList();
            return spu;
        }

        public List<spu> queryByKeyword(string searchstring)
        {
            var spu = db.spu.Where(s => s.spuName.Contains(searchstring) && s.spuShow == "已上架").ToList();

            return spu;
        }

    }
}