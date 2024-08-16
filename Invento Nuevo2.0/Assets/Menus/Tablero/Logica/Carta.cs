using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logica
{
    public enum Clasificacion
    {
        CuerpoaCuerpo = 0b_001,
        LargaDistancia = 0b_010,
        Asedio = 0b_100,
    }

    public enum Faccion
    {
        PrimeraFaccion = 0,
        SegudaFaccion = 1,

        TerceraFaccion = 2,

    }

    public abstract class BaseCard
    {
        public string namecard;
        public string ability;
        public Faccion card;

        public BaseCard(string nombre, string habilidad, Faccion carta)
        {
            namecard = nombre;
            ability = habilidad;
            card = carta;
        }
        public override string ToString()
        {
            return $"{namecard}";
        }
    }

    public abstract class MonsterCard : BaseCard
    {
        public int ataque;
        public uint clasificacion;

        protected MonsterCard(string nombre, string habilidad, Faccion carta, int ataque ,uint numero) : base(nombre, habilidad, carta)
        {
            this.ataque = ataque;
            clasificacion = numero;
        }
    }

    public class Aumento : BaseCard
    {
        public Aumento(string nombre, string habilidad, Faccion carta) : base(nombre, habilidad, carta)
        {
        }
    }

    public class Senuelo : BaseCard
    {
        public Senuelo(string nombre, string habilidad, Faccion carta) : base(nombre, habilidad, carta)
        {
        }
    }
    
    public class Clima : BaseCard
    {
        public Clima(string nombre, string habilidad, Faccion carta) : base(nombre, habilidad, carta)
        {
        }
    }
    
    public class Normales : MonsterCard
    {
        public Normales(string nombre, string habilidad, Faccion carta, int ataque, uint numero) : base(nombre, habilidad, carta, ataque, numero)
        {
        }
    }
    
    public class Heroe : MonsterCard
    {
        public Heroe(string nombre, string habilidad, Faccion carta, int ataque, uint numero) : base(nombre, habilidad, carta, ataque, numero)
        {
        }
    }

}
