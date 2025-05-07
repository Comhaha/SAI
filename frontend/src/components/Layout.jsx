import Header from './Header';
import Footer from './Footer';
import Loading from './Loading';
import usePageLoading from '@/hooks/usePageLoading';
import { useLocation } from 'react-router-dom';

function Layout({ children }) {
  const isLoading = usePageLoading('/screenshot', 1000);
  const location = useLocation();
  const isHome = location.pathname === '/';

  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      {isLoading && <Loading />}
      <main
        className={
          isHome
            ? "flex w-full min-w-[480px] mx-auto px-0"
            : "flex w-full max-w-[1280px] min-w-[480px] mx-auto px-[15px] min-[1130px]:px-[72px] min-[900px]:px-[30px] min-[480px]:px-[15px]"
        }
      >
        {children}
      </main>
      <Footer />
    </div>
  );
}

export default Layout;
