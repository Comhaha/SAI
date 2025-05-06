import Header from './Header';
import Footer from './Footer';
import Loading from './Loading';
import usePageLoading from '@/hooks/usePageLoading';

function Layout({ children }) {
  const isLoading = usePageLoading('/screenshot', 1000);

  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      {isLoading && <Loading />}
      <main className="flex-1 w-full max-w-[1280px] mx-auto px-[72px] md:max-w-full md:px-[30px]">
        {children}
      </main>
      <Footer />
    </div>
  );
}

export default Layout;
