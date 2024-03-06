// Composables
import { createRouter, createWebHistory } from 'vue-router'
import { AuthStore } from '@/store'



const routes = [
  {
    path: '/',
    component: () => import('@/views/Home.vue'),
    children: [
      {
        path: '/cool',
        name: 'Cool',
        component: () => import('@/views/Home.vue'),
      }
    ],
  },
  {
    path: '/about',
    name: 'About',
    component: () => import('@/views/About.vue'),
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/Login.vue'),
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('@/views/Register.vue'),
    meta: {
      requiresAuth: true 
    }
  },
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('@/views/Admin/Team.vue'),
    meta: {
      requiresAuth: true 
    }
  },
  {
    path: '/game/:id',
    name: 'Game',
    component: () => import('@/views/Game.vue'),
  },
  {
    path : '/result/:id',
    name : 'Result',
    component : () => import('@/views/Result.vue'),
  },
  {
    path : '/logout',
    name : 'logout',
    component : () => import('@/views/Home.vue'),
  },
  {
    path : '/card/:id',
    name : 'mycard',
    component : () => import('@/views/Card.vue'),
    meta: {
      requiresAuth: true 
    }
  },
  {
    path : '/acards',
    name : 'amycards',
    component : () => import('@/views/ACards.vue'),
    meta: {
      requiresAuth: true 
    }
  },
  {
    path : '/acard/:id',
    name : 'amycard',
    component : () => import('@/views/ACard.vue'),
    meta: {
      requiresAuth: true 
    }
  },
  {
    path : '/cards',
    name : 'mycards',
    component : () => import('@/views/Cards.vue'),
    meta: {
      requiresAuth: true 
    }
  },
  {
    path : '/test',
    name : 'test',
    component : () => import('@/views/Test.vue'),
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/NotFound.vue'),
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

router.beforeEach((to, from, next) => {
  // console.log('to', to.name);
  const authStore = AuthStore();
  if(to.name == 'logout'){
    localStorage.removeItem('user');
  }

  if (to.meta.requiresAuth) {
    
    if (authStore.user && authStore.checkToken()) {
      // User is authenticated, proceed to the route
      next();
    } else {
     
      // User is not authenticated, redirect to login
      authStore.returnUrl = to.fullPath;
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    }
  } else {
    // Non-protected route, allow access
    next();
  }
});

export default router
