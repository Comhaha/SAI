import content11 from '@/assets/images/content1-1.svg';
import content12 from '@/assets/images/content1-2.svg';
import content13 from '@/assets/images/content1-3.svg';
import content14 from '@/assets/images/content1-4.svg';
import Card from '@/components/Card';

function Content2() {
  return (
    <div className="w-full flex justify-between items-start gap-8 max-[900px]:flex-col">
      {/* 왼쪽 영역 */}
      <div className="flex flex-col w-1/2 max-[900px]:w-full h-full">
        <div className="text-[40px] font-bold font-['Noto_Sans_KR'] mb-2 max-[900px]:text-[28px]">
          AI Learning: From Data to Insight
        </div>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 mb-4 max-[900px]:text-[14px]">
          AI learning finds patterns in data and helps you make predictions or classifications. Experience the full process from labeling to training and inference, all in one place.
        </div>
        <div className="flex-1 flex items-stretch">
          <img
            src={content11}
            alt="AI Curve Graph"
            className="w-full max-w-[400px] rounded shadow h-full object-cover"
            style={{ minHeight: 0 }}
          />
        </div>
      </div>

      {/* 오른쪽 영역 */}
      <div className="flex flex-col w-1/2 gap-6 justify-between h-full max-[900px]:w-full max-[900px]:gap-4">
        <Card
          title="AI Labeling"
          description="Add meaningful tags to your data so AI can learn. Good labeling leads to better models."
          image={content12}
          alt="AI Labeling"
        />
        <Card
          title="AI Training"
          description="Train your model with prepared data. Adjust settings and find the best solution for your task."
          image={content13}
          alt="AI Training"
        />
        <Card
          title="AI Inference"
          description="Use your trained model to predict, classify, or recommend. See results instantly and visually."
          image={content14}
          alt="AI Inference"
          borderBottom={false}
        />
      </div>
    </div>
  );
}

export default Content2;
