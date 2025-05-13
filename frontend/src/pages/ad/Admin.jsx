import AdminSt from './AdminSt';
import AdminRe from './AdminRe';
import { useAuth } from './AuthContext.jsx';

function Admin() {
  const { isLoggedIn } = useAuth();
  return isLoggedIn ? <AdminRe /> : <AdminSt />;
}

export default Admin;
