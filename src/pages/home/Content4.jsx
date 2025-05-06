import * as React from "react";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel";
import { motion } from "motion/react";
import { useFadeInUp } from "@/hooks/useFadeInUp";

const reviews = [
  {
    name: "Jane Kim, Data Scientist",
    avatar: "https://randomuser.me/api/portraits/women/44.jpg",
    text: "Using this platform, I could easily label and train my AI models without any coding. The tutorials made everything simple and intuitive!",
  },
  {
    name: "Alex Lee, University Student",
    avatar: "https://randomuser.me/api/portraits/men/32.jpg",
    text: "Block coding for AI was a game changer for my class project. I could visualize every step and share results with my team instantly.",
  },
  {
    name: "Sophia Park, Researcher",
    avatar: "https://randomuser.me/api/portraits/women/68.jpg",
    text: "The auto-generated reports saved me hours. I loved how I could see all my data and predictions in one place, ready to present.",
  },
  {
    name: "Michael Choi, Teacher",
    avatar: "https://randomuser.me/api/portraits/men/65.jpg",
    text: "My students learned AI concepts much faster with hands-on tutorials and visual tools. Highly recommended for education!",
  },
];

function Content4() {
  const fadeInUpText = useFadeInUp(0);
  const fadeInUpAvatar = useFadeInUp(0.1);

  return (
    <div className="w-full flex justify-center items-start gap-12 py-16 max-w-[1296px] mx-auto">
      {/* Left: Section Title and Intro */}
      <div className="flex-1 min-w-[320px] max-w-[500px]">
        <div className="text-[40px] font-bold font-['Noto_Sans_KR'] text-black mb-4">AI Learning, Real User Stories</div>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 mb-6">
          Discover how people from all backgrounds use our AI learning platform to achieve their goals. Read real reviews from students, researchers, and professionals.
        </div>
        <a href="#" className="text-[#2878BE] hover:underline text-[17px] font-medium font-['Noto_Sans_KR'] flex items-center w-fit">
          Learn more
          <svg className="ml-1" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#2878BE" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M9 18l6-6-6-6"/></svg>
        </a>
      </div>
      {/* Right: Carousel */}
      <div className="flex-1 min-w-[340px] max-w-[600px]">
        <Carousel autoplay autoplayInterval={3000} opts={{ loop: true }}>
          <CarouselContent>
            {reviews.map((review, idx) => (
              <CarouselItem key={idx}>
                <div className="p-4">
                  <motion.div
                    {...fadeInUpText}
                    className="rounded-lg shadow-sm border bg-white p-6 flex flex-col gap-6"
                  >
                    <div className="text-[17px] text-gray-800 font-medium mb-4">{review.text}</div>
                    <div className="flex items-center gap-4 mt-2">
                      <motion.img
                        {...fadeInUpAvatar}
                        src={review.avatar}
                        alt={review.name}
                        className="w-12 h-12 rounded-full object-cover border"
                      />
                      <div>
                        <div className="font-bold text-[16px] text-black">{review.name}</div>
                      </div>
                    </div>
                  </motion.div>
                </div>
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious />
          <CarouselNext />
        </Carousel>
      </div>
    </div>
  );
}

export default Content4;
