// import AccessAlarmIcon from '@mui/icons-material/AccessAlarm';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from '@//components/Layout';
import Home from '@/pages/home/Home';
import Download from '@/pages/download/Download';
import Admin from '@/pages/ad/Admin';
import Login from '@/pages/ad/Login';
import ErrorPage from '@/pages/ad/ErrorPage';
import AdminRe from '@/pages/ad/AdminRe';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/download" element={<Download />} />
          <Route path="/admin" element={<Admin />} />
          <Route path="/admin/login" element={<Login />} />
          <Route path="/test" element={<AdminRe />} />
          <Route path="*" element={<ErrorPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
