import content21 from '@/assets/images/content2-1.svg';
import content22 from '@/assets/images/content2-2.svg';
import content23 from '@/assets/images/content2-3.svg';
import Card2 from '@/components/Card2';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronRight } from '@fortawesome/free-solid-svg-icons';

function Content3() {
    return (
        <div className="w-full flex flex-col gap-24 py-8">
            <Card2
                title="Learn AI with Tutorials and Practice"
                description="Load datasets and experience the entire AI learning process through guided tutorials and hands-on practice. Step-by-step instructions make it easy for anyone to get started with AI."
                image={content21}
                alt="AI Tutorial Practice"
                imagePosition="right"
            >
                <div className="flex gap-4 text-[15px] font-medium font-['Noto_Sans_KR']">
                    <a href="#" className="text-[#2878BE] hover:underline flex items-center">
                        Learn more
                        <FontAwesomeIcon icon={faChevronRight} className="ml-1 text-[#2878BE] text-[8px] align-middle" />
                    </a>
                    <a href="#" className="text-[#2878BE] hover:underline flex items-center">
                        Watch tutorial
                        <FontAwesomeIcon icon={faChevronRight} className="ml-1 text-[#2878BE] text-[8px] align-middle" />
                    </a>
                </div>
            </Card2>
            <Card2
                title="AI Block Coding"
                description="Build AI models visually by assembling blocks. Easily perform data analysis and prediction without coding, and intuitively understand how AI works."
                image={content22}
                alt="AI Block Coding"
                imagePosition="left"
            >
                <a href="#" className="text-[#2878BE] hover:underline text-[15px] font-medium font-['Noto_Sans_KR'] w-fit flex items-center">
                    Learn more
                    <FontAwesomeIcon icon={faChevronRight} className="ml-1 text-[#2878BE] text-[8px] align-middle" />
                </a>
            </Card2>
            <Card2
                title="AI Report after Block Coding"
                description="Automatically generate reports from your completed AI block coding projects. Instantly see visualized data and analysis results in a clear, organized format."
                image={content23}
                alt="AI Report"
                imagePosition="right"
            >
                <a href="#" className="text-[#2878BE] hover:underline text-[15px] font-medium font-['Noto_Sans_KR'] w-fit flex items-center">
                    View sample report
                    <FontAwesomeIcon icon={faChevronRight} className="ml-1 text-[#2878BE] text-[8px] align-middle" />
                </a>
            </Card2>
        </div>
    )
}

export default Content3;
