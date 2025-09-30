using System;
using System.Diagnostics;
using System.Threading;

namespace _250929
{
    public enum BuffType
    {
        PositiveBuff,       //버프
        NegativeBuff        //디버프
    }

    /// <summary>
    /// 각각의 버프를 정의하는 추상 클래스
    /// </summary>
    public abstract class Buff
    {
        public BuffType buffType { get; protected set; }
        public bool isApply { get; private set; }           //현재 버프가 적용중인지 판단하는 변수 / isApply가 true고 Duration이 0일 때 -> 버프가 종료되는 순간 버프 종료 메서드 실행

        public string buff_Name { get; private set; }       //이름
        private float buff_DefaultDuration;                 //버프 지속시간 (버프 초기화시 buff_DefaultDuration 만큼 buff_Duration을 초기화)
        private float buff_Duration;                        //현재 버프의 지속시간 (타이머에 따라 실제 변경될 지속시간 변수)
        public float Buff_Duration                          //현재 버프 지속시간 프로퍼티
        {
            get { return buff_Duration; }
            set
            {
                if (value < 0f)
                {
                    value = 0f;

                    if (isApply)
                        isApply = false;
                }
                else if (value > buff_DefaultDuration)
                {
                    value = buff_DefaultDuration;
                }

                //소수점 한자리로 보간
                buff_Duration = (float)Math.Round(value, 1);
            }
        }

        //생성자 - 각 필드 초기화
        public Buff(string buff_Name, float duration)
        {
            this.buff_Name = buff_Name;
            buff_DefaultDuration = buff_Duration = duration;
            isApply = true;
        }

        /// <summary>
        /// 버프 초기화, 객체를 따로 재생성하지 않고 재사용하기 위함
        /// 추후 각각의 버프에서 따로 초기화해야 할 내용을 대비해 가상함수로 선언
        /// </summary>
        public virtual void InitBuff()
        {
            buff_Duration = buff_DefaultDuration;
            isApply = true;
        }

        /// <summary>
        /// 실 적용될 버프의 효과
        /// 각각의 버프들은 효과가 다르기 때문에 추상 메서드로 선언
        /// </summary>
        public abstract void ApplyBuff();
    }

    #region 버프 클래스들
    /// <summary>
    /// 플레이어에게 이로운 버프 추상 클래스
    /// </summary>
    public abstract class PositiveBuff : Buff
    {
        //생성자
        public PositiveBuff(string buff_Name, float duration) : base(buff_Name, duration)
        {
            buffType = BuffType.PositiveBuff;
        }

        public virtual void Test()
        {
            Console.WriteLine("부모");
        }
    }

    public class Buff_SpeedUp : PositiveBuff
    {
        public Buff_SpeedUp(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"이동속도 증가!");
        }

        public override void Test()
        {
            base.Test();
            Console.WriteLine("자식");
        }
    }

    public class buff_Invincible : PositiveBuff
    {
        public buff_Invincible(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"일정시간동안 무적!");
        }
    }

    public class buff_RangeUp : PositiveBuff
    {
        public buff_RangeUp(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"사거리 증가!");
        }
    }

    public class buff_Reaper : PositiveBuff
    {
        public buff_Reaper(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"리퍼 버프좀요!");
        }
    }

    #endregion

    #region 디버프 클래스들
    /// <summary>
    /// 플레이어에게 해로운 버프 추상 클래스
    /// </summary>
    public abstract class NegativeBuff : Buff
    {
        //생성자
        public NegativeBuff(string buff_Name, float buff_Duration) : base(buff_Name, buff_Duration)
        {
            buffType = BuffType.NegativeBuff;
        }
    }

    public class debuff_Poisoned : NegativeBuff
    {
        public debuff_Poisoned(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"독에 감염되었습니다!");
        }
    }

    public class debuff_Stunned : NegativeBuff
    {
        public debuff_Stunned(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"기절 상태입니다!");
        }
    }

    public class debuff_Burning : NegativeBuff
    {
        public debuff_Burning(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"불에 타고있습니다!");
        }
    }

    public class debuff_Frozen : NegativeBuff
    {
        public debuff_Frozen(string buff_Name, float duration) : base(buff_Name, duration) { }

        public override void ApplyBuff()
        {
            Console.WriteLine($"빙결 상태에 걸려 이속이 감소됩니다!");
        }
    }
    #endregion
}
