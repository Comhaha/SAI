using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
	public class BlocklyModel
	{
		private static BlocklyModel instance;

		// 전체 블록 코드 변경시 발생하는 이벤트
		public event Action<string> BlockAllCodeChanged;
		// 전체 블록에서 반환되는 Python 코드
		private string _blockAllCode;

		// 개별 블록 코드 변경시 발생하는 이벤트
		public event Action<string> BlockCodeChanged;
		// 개별 블록 코드
		private string _blockCode;

		// 이미지 경로
		private string _imgPath;

		// start 블럭에 붙어있는 모든 블럭들
		private List<BlockInfo> _blockTypes;

        // 추론 정확도 값 추가
        private double _accuracy;

		// 모델
		private string _model;

		// epoch
		private int _epoch;

		// imgsz
		private int _imgsz;

		// _Conv
		private int _Conv;

		// _C2f
		private int _C2f;

		// _Upsample_scale
		private double _Upsample_scale;


		// private 생성자 -> 외부에서 생성 못하게.
		private BlocklyModel()
		{
			_blockAllCode = "";
			_blockCode = "";
		}

		// [싱글톤] 외부에서 BlocklyModel 인스턴스를 가져올 수 있게
		public static BlocklyModel Instance
		{
			get { 
				if (instance == null)
				{
					instance = new BlocklyModel();
				}
				return instance;
			}
		}

		public string blockAllCode
		{
			get => _blockAllCode;
			set
			{
				if (_blockAllCode != value)
				{
					_blockAllCode = value;
					BlockAllCodeChanged?.Invoke(_blockAllCode); // 이벤트 발생
				}
			}
		}

		public string blockCode
		{
			get => _blockCode;
			set
			{
				if (_blockCode != value)
				{
					_blockCode = value;
					BlockCodeChanged?.Invoke(_blockCode); // 이벤트 발생
				}
			}
		}


		public string imgPath
		{
			get => _imgPath;
			set
			{
				if (_imgPath != value)
				{
					_imgPath = value;
					Console.WriteLine($"[INFO] 새로운 이미지 경로 설정됨: {value}");
				}
			}
		}
		public class BlockInfo
		{
			public string type { get; set; }
			public List<BlockInfo> children { get; set; }  // 있을 수도 없을 수도 있음
		}


		public List<BlockInfo> blockTypes
		{
			get => _blockTypes;
			set
			{
				if (_blockTypes != value)
				{
					_blockTypes = value;
				}
			}
		}

        // accuracy 속성 추가
        public double accuracy
        {
            get => _accuracy;
            set
            {
                if (_accuracy != value)
                {
                    _accuracy = value;
                }
            }
        }

		public string model
		{
			get => _model;
			set
			{
				if (_model != value)
				{
					_model = value;
				}
			}
		}

		public int epoch
		{
			get => _epoch;
			set
			{
				if (_epoch != value)
				{
					_epoch = value;
				}
			}
		}

		public int imgsz
		{
			get => _imgsz;
			set
			{
				if (_imgsz != value)
				{
					_imgsz = value;
				}
			}
		}


		public int Conv
		{
			get => _Conv;
			set
			{
				if (_Conv != value)
				{
					_Conv = value;
				}
			}
		}

		public int C2f
		{
			get => _C2f;
			set
			{
				if (_C2f != value)
				{
					_C2f = value;
				}
			}
		}

		public double Upsample_scale
		{
			get => _Upsample_scale;
			set
			{
				if (_Upsample_scale != value)
				{
					_Upsample_scale = value;
				}
			}
		}
	}
}
