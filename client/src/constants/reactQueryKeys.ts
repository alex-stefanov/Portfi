// import { routes } from './routes';

// -- Tanstack Query cache resets/invalidation relays on the current path. --
// The routes are used as keys because the cache invalidation is using the current pathname to find which route needs to be invalidated when using filters (artstyles/search, etc).
// Do not touch them, otherwise you'll mess up the caching mechanism

export const reactQueryKeys = {
  user: {
    fullProfile: () => ['full-profile'] as const,
    session: () => ['session'] as const,
  },
  // images: {
  //   KEYS: {
  //     ALL: ['all-images-key'] as const,
  //     FILTERED: ['filtered-images-key'] as const,
  //     FAVORITES: ['favorite-images-key'] as const,
  //   },
  //   byId: (id: string) => ['image', id] as const,

  //   public: () => [routes.discover.path, ...reactQueryKeys.images.KEYS.ALL] as const,

  //   publicFiltered: () =>
  //     [
  //       ...reactQueryKeys.images.public(),
  //       ...reactQueryKeys.images.KEYS.FILTERED,
  //     ] as const,

  //   userOwnCollection: () =>
  //     [
  //       routes.user.subroutes.collection.path,
  //       ...reactQueryKeys.images.KEYS.ALL,
  //     ] as const,

  //   userOwnCollectionFiltered: () =>
  //     [
  //       ...reactQueryKeys.images.userOwnCollection(),
  //       ...reactQueryKeys.images.KEYS.FILTERED,
  //     ] as const,

  //   userFavorites: () =>
  //     [
  //       routes.user.subroutes.favorites.path,
  //       ...reactQueryKeys.images.KEYS.ALL,
  //       ...reactQueryKeys.images.KEYS.FAVORITES,
  //     ] as const,

  //   userFavoritesFiltered: () =>
  //     [
  //       ...reactQueryKeys.images.userFavorites(),
  //       ...reactQueryKeys.images.KEYS.FILTERED,
  //     ] as const,

  //   userPublicCollection: (id: string) =>
  //     [routes.user.path, id, ...reactQueryKeys.images.KEYS.ALL] as const,

  //   userPublicCollectionFiltered: (id: string) =>
  //     [
  //       ...reactQueryKeys.images.userPublicCollection(id),
  //       ...reactQueryKeys.images.KEYS.FILTERED,
  //     ] as const,
  // },
};
