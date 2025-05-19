import * as React from 'react';
import Content1 from './Content1';
import Content2 from './Content2';
import Content3 from './Content3';
import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';

function Home() {
  const location = useLocation();
  useEffect(() => {
    if (location.state && location.state.scrollTo === 'content2') {
      const el = document.getElementById('content2');
      if (el) {
        el.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }
    }
  }, [location]);

  return (
    <div className="flex flex-col w-full">
      <Content1 />
      <Content2 />
      <Content3 />
    </div>
  );
}

export default Home;
